using BusinessLogic.DTOs.Candidate;
using BusinessLogic.Mappers;
using CommonSolution.Constants;
using DataAccess.Context;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        public void AddCandidateStep(ProcedureStepDTO dto)
        {
            ProcedureStep entity = _mapper.MapToEntity(dto);
            entity.AddRow = DateTime.Now;

            _context.ProcedureStep.Add(entity);
        }

        #endregion

        #region UPDATE

        public void UpdateCandidate(CandidateCreationDTO candidate, UnitOfWork uow, decimal userId)
        {
            Candidate entity = _context.Candidate.FirstOrDefault(x => x.Id == candidate.Id);

            entity = this._mapper.MapToEditEntity(candidate, entity);
            entity.UpdRow = DateTime.Now;

            _context.Candidate.Update(entity);
            uow.LogRepository.LogCandidate(entity, userId, CActions.edit);
        }


        #endregion

        #region DELETE


        #endregion

        #region ANY
        public bool ExistCandidateById(decimal id)
        {
            return _context.Candidate.Any(x => x.Id == id);
        }

        public bool ExistCandidateByDocument(string document)
        {
            return _context.Candidate.Any(x => x.PersonalDocument == document);
        }

        #endregion

        #region GET

        public IQueryable<VCandidate> GetCandidates(string search)
        {
            return _context.VCandidate.AsNoTracking().Where(x => x.LastName.ToLower().Contains(search.ToLower())|| x.PersonalDocument.ToLower().Contains(search.ToLower())).AsQueryable();
        }
        public CandidateCreationFrontDTO GetCandidateCreationById(decimal id)
        {
            var x = _context.VCandidate.FirstOrDefault(x => x.Id == id);
            return _mapper.MapToEditObject(x);
        }

        public CandidateCreationDTO GetCandidateById(decimal id)
        {
            var x = _context.Candidate.FirstOrDefault(x => x.Id == id);
            return _mapper.MapToObject(x);
        }

        public List<ProcedureStepDTO> GetCandidateStepsById(int idCandidate)
        {
            var x = _context.ProcedureStep.Where(x => x.IdCandidate == idCandidate).ToList();
            return _mapper.MapToObject(x);
        }

        public List<decimal?> GetDependentCandiateNumbers()
        {
            return _context.VDependentCandidateNumbers.Select(s => s.Number).OrderBy(o => o).ToList();
        }

        public List<string> GetNeighborhoods()
        {
            return _context.DecisionParam.Where(x => x.ActiveFlag == "S" && x.Name != "General").Select(s => s.Name).ToList();
        }

        #endregion

    }
}
