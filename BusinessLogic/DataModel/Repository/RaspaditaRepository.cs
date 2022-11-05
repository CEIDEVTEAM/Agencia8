using BusinessLogic.DTOs.Raspadita;
using BusinessLogic.Mappers;
using DataAccess.Context;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.DataModel.Repository
{
    public class RaspaditaRepository
    {
        private readonly Agencia_8Context _context;
        private readonly RaspaditaMapper _mapper;

        public RaspaditaRepository(Agencia_8Context context)
        {
            this._context = context;
            this._mapper = new RaspaditaMapper();
        }

        #region ADD

        public decimal AddRaspadita(RaspaditaDTO dto)
        {
            Raspadita entity = _mapper.MapToEntity(dto);
            entity.AddRow = DateTime.Now;

            _context.Raspadita.AddAsync(entity);

            return entity.Id;
        }

        #endregion

        #region UPDATE

        public void UpdateRaspadita(RaspaditaDTO raspadita)
        {
            Raspadita entity = _context.Raspadita.FirstOrDefault(x => x.Id == raspadita.Id);

            entity = this._mapper.MapToEditEntity(raspadita, entity);
            entity.UpdRow = DateTime.Now;

            _context.Raspadita.Update(entity);
        }


        #endregion

        #region DELETE


        #endregion

        #region ANY
        public bool ExistRaspaditaById(decimal id)
        {
            return _context.Raspadita.Any(x => x.Id == id);
        }

        #endregion

        #region GET

        public IQueryable<Raspadita> GetRaspaditas(UnitOfWork uow)
        {
            decimal? periodId = uow.PeriodRepository.GetActivePeriod();

            return _context.Raspadita.AsNoTracking().Where(x => x.PeriodId == (periodId ?? -1)).AsQueryable();
        }

        public RaspaditaDTO GetRaspaditaById(decimal id)
        {
            var x = _context.Raspadita.FirstOrDefault(x => x.Id == id);
            return _mapper.MapToObject(x);
        }

        #endregion

    }
}
