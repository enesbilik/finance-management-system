using System.Security.Claims;
using System.Transactions;
using Application.Services.Repositories;
using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Transfer.Commands.Create;

public class CreateTransferCommand : IRequest<CreatedTransferResponse>

{
    public Guid FromUserId { get; set; }

    public Guid ToUserId { get; set; }

    public decimal Amount { get; set; }

    public class
        CreateTransferCommandHandler : IRequestHandler<CreateTransferCommand,
            CreatedTransferResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITransferRepository _transferRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public CreateTransferCommandHandler(UserManager<AppUser> userManager,
            ITransferRepository transferRepository,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _transferRepository = transferRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<CreatedTransferResponse> Handle(CreateTransferCommand request,
            CancellationToken cancellationToken)
        {
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                var userId =
                    _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (userId == null)
                {
                    throw new BusinessException("User not found");
                }

                if (request.FromUserId != Guid.Parse(userId))
                {
                    throw new BusinessException("Unauthorized");
                }

                var fromUser = await _userManager.FindByIdAsync(request.FromUserId.ToString());
                var toUser = await _userManager.FindByIdAsync(request.ToUserId.ToString());

                if (fromUser == null || toUser == null)
                {
                    throw new BusinessException("To user not found");
                }

                if (request.Amount <= 0)
                {
                    throw new BusinessException("Invalid transfer amount");
                }


                if (request.FromUserId == request.ToUserId)
                {
                    throw new BusinessException("Cannot transfer to the same account");
                }


                if (fromUser.Balance < request.Amount)
                {
                    throw new BusinessException("Insufficient balance");
                }


                var transfer = new Domain.Entities.Transfer
                {
                    FromUserId = request.FromUserId,
                    ToUserId = request.ToUserId,
                    Amount = request.Amount,
                    TransferStatus = TransferStatus.Created
                };


                await _transferRepository.AddAsync(transfer);
                


                // Update user balances
                fromUser.Balance -= request.Amount;
                toUser.Balance += request.Amount;

                await _userManager.UpdateAsync(fromUser);
                await _userManager.UpdateAsync(toUser);


                transfer.TransferStatus = TransferStatus.Success;
                await _transferRepository.UpdateAsync(transfer);

                var createdTransferResponse = _mapper.Map<CreatedTransferResponse>(transfer);

                scope.Complete();
                
                throw new BusinessException("Deneme");

                return createdTransferResponse;
            }
            catch (Exception)
            {
                // Handle failure
                var transfer = new Domain.Entities.Transfer
                {
                    FromUserId = request.FromUserId,
                    ToUserId = request.ToUserId,
                    Amount = request.Amount,
                    TransferStatus = TransferStatus.Failed
                };
                await _transferRepository.AddAsync(transfer);
                throw new BusinessException("Transfer failed");
            }
        }
    }
}