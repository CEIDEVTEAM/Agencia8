using BusinessLogic.DTOs.Concept;
using BusinessLogic.DTOs.Period;
using BusinessLogic.Mappers;
using DataAccess.Context;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.DataModel.Repository
{
    public class PeriodRepository
    {
        private readonly Agencia_8Context _context;
        private readonly PeriodMapper _mapper;

        public PeriodRepository(Agencia_8Context context)
        {
            this._context = context;
            this._mapper = new PeriodMapper();
        }

        #region ADD

        public decimal AddPeriod(PeriodDTO dto)
        {
            Period entity = _mapper.MapToEntity(dto);
            entity.AddRow = DateTime.Now;

            _context.Period.AddAsync(entity);

            return entity.Id;
        }

        #endregion

        #region UPDATE

        public void UpdatePeriod(PeriodDTO concept)
        {
            Period entity = _context.Period.FirstOrDefault(x => x.Id == concept.Id);

            entity = this._mapper.MapToEditEntity(concept, entity);
            entity.UpdRow = DateTime.Now;

            _context.Period.Update(entity);
        }


        #endregion

        #region DELETE


        #endregion

        #region ANY
        public bool ExistPeriodById(decimal id)
        {
            return _context.Period.Any(x => x.Id == id);
        }

        #endregion

        #region GET

        public decimal GetActivePeriod()
        {
            return _context.Period.FirstOrDefault(x => x.ActiveFlag == "S").Id;
        }

        public IQueryable<Period> GetPeriods()
        {
            return _context.Period.AsNoTracking().AsQueryable();
        }

        public ConceptDTO GetPeriodById(decimal id)
        {
            var x = _context.Period.FirstOrDefault(x => x.Id == id);
            return _mapper.MapToObject(x);
        }

        #endregion

    }
}
