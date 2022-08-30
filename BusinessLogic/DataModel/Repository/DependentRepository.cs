using BusinessLogic.DTOs.Dependent;
using BusinessLogic.Mappers;
using CommonSolution.Constants;
using DataAccess.Context;
using DataAccess.Models;

namespace BusinessLogic.DataModel.Repository
{
    public class DependentRepository
    {
        private readonly Agencia_8Context _context;
        private readonly DependentMapper _mapper;

        public DependentRepository(Agencia_8Context context)
        {
            this._context = context;
            this._mapper = new DependentMapper();
        }

        #region ADD

        public decimal AddDependent(DependentCreationDTO dto, UnitOfWork uow, decimal userId)
        {
            Dependent entity = _mapper.MapToEntity(dto);
            entity.AddRow = DateTime.Now;

            _context.Dependent.AddAsync(entity);
            uow.LogRepository.LogDependent(entity, userId, CActions.add);

            return entity.Id;
        }

        #endregion

        #region UPDATE

        public void UpdateDependent(DependentCreationDTO dependent, UnitOfWork uow, decimal userId)
        {
            Dependent entity = this.GetDependentByNumber(dependent.Number);

            entity = this._mapper.MapToEditEntity(dependent, entity);
            entity.UpdRow = DateTime.Now;
            entity.UpdUserId = userId;

            _context.Dependent.Update(entity);
            uow.LogRepository.LogDependent(entity, userId, CActions.edit);
        }


        #endregion

        #region DELETE

        public void DeleteDependent(decimal number, UnitOfWork uow, decimal userId)
        {
            Dependent entity = GetDependentByNumber(number);

            _context.Dependent.Remove(entity);
            uow.LogRepository.LogDependent(entity, userId, CActions.edit);
        }


        #endregion

        #region ANY
        public bool ExistDependentByNumber(decimal number)
        {
            return _context.Dependent.Any(x => x.Number == number);
        }

        #endregion

        #region GET

        public IQueryable<VDependent> GetDependents()
        {
            return _context.VDependent.AsQueryable();
        }

        public Dependent GetDependentByNumber(decimal number)
        {
            return _context.Dependent.FirstOrDefault(x => x.Number == number);
        }

        #endregion

    }
}
