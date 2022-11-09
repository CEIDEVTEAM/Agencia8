using BusinessLogic.DTOs.Concept;
using BusinessLogic.DTOs.Generals;
using BusinessLogic.Mappers;
using DataAccess.Context;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.DataModel.Repository
{
    public class ConceptRepository
    {
        private readonly Agencia_8Context _context;
        private readonly ConceptMapper _mapper;

        public ConceptRepository(Agencia_8Context context)
        {
            this._context = context;
            this._mapper = new ConceptMapper();
        }

        #region ADD

        public decimal AddConcept(ConceptDTO dto)
        {
            Concept entity = _mapper.MapToEntity(dto);
            entity.AddRow = DateTime.Now;

            _context.Concept.AddAsync(entity);

            return entity.Id;
        }

        #endregion

        #region UPDATE

        public void UpdateConcept(ConceptDTO concept)
        {
            Concept entity = _context.Concept.FirstOrDefault(x => x.Id == concept.Id);

            entity = this._mapper.MapToEditEntity(concept, entity);
            entity.UpdRow = DateTime.Now;

            _context.Concept.Update(entity);
        }


        #endregion

        #region DELETE

        public void DeleteConceptById(int concept)
        {
            Concept entity = _context.Concept.FirstOrDefault(x => x.Id == concept);

            if (entity != null)
                _context.Concept.Remove(entity);
        }


        #endregion

        #region ANY
        public bool ExistConceptById(decimal id)
        {
            return _context.Concept.Any(x => x.Id == id);
        }

        public bool ExistConceptByParamAndPeriod(decimal paramId, decimal periodId)
        {
            return _context.Concept.Any(x => x.ParamId == paramId && x.PeriodId == periodId);
        }

        #endregion

        #region GET

        public IQueryable<VConcept> GetConcepts(UnitOfWork uow)
        {
            decimal? periodId = uow.PeriodRepository.GetActivePeriod();

            return _context.VConcept.AsNoTracking().Where(x=> x.PeriodId == periodId).AsQueryable();
        }

        public ConceptCompleteDTO GetConceptById(decimal id)
        {
            var x = _context.VConcept.FirstOrDefault(x => x.Id == id);
            return _mapper.MapToObject(x);
        }

        public decimal? GetProjectionParamValueByName(string name)
        {
            decimal? paramId = _context.ProjectionParam.FirstOrDefault(x => x.Name == name).Id;

            return paramId == null ? null : _context.Concept.FirstOrDefault(x => x.ParamId == paramId).Value;
        }

        #endregion

    }
}
