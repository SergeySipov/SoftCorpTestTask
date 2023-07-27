using System.Security.Authentication;
using Application.Interfaces.Repositories;
using Application.Interfaces.Repositories.Common;
using Application.Interfaces.UnitOfWork;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.UserFeatures.Commands.RevokeAccessToken;

public record RevokeAccessTokenCommand : IRequest
{
    public string RefreshToken { get; init; }
}

internal class RevokeAccessTokenCommandHandler : IRequestHandler<RevokeAccessTokenCommand>
{
    private readonly IUserRefreshTokenRepository _userRefreshTokenRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBaseRepository<UserRefreshToken> _userBaseRefreshTokenRepository;

    public RevokeAccessTokenCommandHandler(IUserRefreshTokenRepository userRefreshTokenRepository, 
        IUnitOfWork unitOfWork, 
        IBaseRepository<UserRefreshToken> userBaseRefreshTokenRepository)
    {
        _userRefreshTokenRepository = userRefreshTokenRepository;
        _unitOfWork = unitOfWork;
        _userBaseRefreshTokenRepository = userBaseRefreshTokenRepository;
    }

    public async Task Handle(RevokeAccessTokenCommand request, CancellationToken cancellationToken)
    {
        var userRefreshToken = await _userRefreshTokenRepository.GetUserRefreshTokenAsync(Guid.Parse(request.RefreshToken));
        if (userRefreshToken == null)
        {
            throw new AuthenticationException();
        }

        _userBaseRefreshTokenRepository.Delete(userRefreshToken);
        await _unitOfWork.SaveAsync(cancellationToken);
    }
}