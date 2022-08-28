using BusinessLogic.DTOs.Candidate;
using BusinessLogic.Mappers;
using CommonSolution.Constants;
using DataAccess.Context;
using DataAccess.Models;

namespace BusinessLogic.DataModel.Repository
{
    public class CandidateRepository
    {
        private readonly Agencia_8Context _context;
        private readonly CandidateMapper _mapper;

        public CandidateRepository(Agencia_8Context context)
        {
            this._context = context;
            this._mapper = new CandidateMapper();
        }

        #region ADD

        public decimal AddCandidate(CandidateCreationDTO dto, UnitOfWork uow, decimal userId)
        {
            Candidate entity = _mapper.MapToEntity(dto);
            entity.AddRow = DateTime.Now;

            _context.Candidate.AddAsync(entity);
            uow.LogRepository.LogCandidate(entity, userId, CActions.add);

            return entity.Id;
        }

        #endregion

        #region UPDATE

        public void UpdateCandidate(CandidateCreationDTO candidate, UnitOfWork uow, decimal userId)
        {
            Candidate entity = this.GetCandidateById(candidate.Id);

            entity = this._mapper.MapToEditEntity(candidate, entity);
            entity.UpdRow = DateTime.Now;

            _context.Candidate.Update(entity);
            uow.LogRepository.LogCandidate(entity, userId, CActions.edit);
        }


        #endregion

        #region DELETE

        public void DeleteCandidate(decimal id, UnitOfWork uow, decimal userId)
        {
            Candidate entity = GetCandidateById(id);

            _context.Candidate.Remove(entity);
            uow.LogRepository.LogCandidate(entity, userId, CActions.edit);
        }


        #endregion

        #region ANY
        public bool ExistCandidateById(decimal id)
        {
            return _context.Candidate.Any(x => x.Id == id);
        }

        #endregion

        #region GET

        public IQueryable<Candidate> GetCandidates()
        {
            return _context.Candidate.AsQueryable();
        }

        public Candidate GetCandidateById(decimal id)
        {
            return _context.Candidate.FirstOrDefault(x => x.Id == id);
        }

        #endregion

    }
}
