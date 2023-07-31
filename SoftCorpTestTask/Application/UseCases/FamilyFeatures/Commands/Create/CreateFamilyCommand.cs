using Application.Interfaces.Repositories.Common;
using Application.Interfaces.UnitOfWork;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.FamilyFeatures.Commands.Create;

public record CreateFamilyCommand : IRequest
{
    public string Title { get; init; }
}

internal class CreateFamilyCommandHandler : IRequestHandler<CreateFamilyCommand>
{
    private readonly IBaseRepository<Family> _baseFamilyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateFamilyCommandHandler(IBaseRepository<Family> baseFamilyRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _baseFamilyRepository = baseFamilyRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task Handle(CreateFamilyCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Family>(request);
        _baseFamilyRepository.Add(entity);
        await _unitOfWork.SaveAsync(cancellationToken);
    }
}