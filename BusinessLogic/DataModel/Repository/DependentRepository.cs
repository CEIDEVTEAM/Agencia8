using AutoMapper.Execution;
using BusinessLogic.DTOs.Dependent;
using BusinessLogic.DTOs.Generals;
using BusinessLogic.Mappers;
using CommonSolution.Constants;
using DataAccess.Context;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.DataModel.Repository
{
    public class DependentRepository
    {
        private readonly Agencia_8Context _context;
        private readonly DependentMapper _mapper;
        private readonly ExternalDependentMapper _emapper;

        public DependentRepository(Agencia_8Context context)
        {
            this._context = context;
            this._mapper = new DependentMapper();
            this._emapper = new ExternalDependentMapper();
        }

        #region ADD

        public decimal AddDependent(DependentCreationDTO dto, UnitOfWork uow, decimal userId)
        {
            Dependent entity = _mapper.MapToEntity(dto);
            entity.AddRow = DateTime.Now;
            entity.ActiveFlag = "S";

            _context.Dependent.Add(entity);
            uow.LogRepository.LogDependent(entity, userId, CActions.add);

            return entity.Id;
        }

        public void AddDependentFact(DependentFactCreationFrontDTO dto)
        {
            DependentFact entity = _mapper.MapToEntity(dto);

            entity.AddRow = DateTime.Now;
            _context.DependentFact.Add(entity);
        }
         

        public void AddExternalDependent(ExternalDependentDTO dto)
        {
            ExternalDependent entity = _emapper.MapToEntity(dto);
            entity.AddRow = DateTime.Now;
            entity.ActiveFlag = "S";

            _context.ExternalDependent.Add(entity);
        }

        #endregion

        #region UPDATE

        public void UpdateDependent(DependentCreationDTO dependent, UnitOfWork uow, decimal userId)
        {
            Dependent entity = _context.Dependent.FirstOrDefault(x => x.Id == dependent.Id);

            entity = this._mapper.MapToEditEntity(dependent, entity);
            entity.UpdRow = DateTime.Now;
            entity.UpdUserId = userId;

            _context.Dependent.Update(entity);
            uow.LogRepository.LogDependent(entity, userId, CActions.edit);
        }

        public void UpdateExternalDependent(int? id, ExternalDependentDTO dto)
        {
            ExternalDependent entity = _context.ExternalDependent.FirstOrDefault(x => x.Id == id);
            entity = this._emapper.MapToEditEntity(dto, entity);

            entity.UpdRow = DateTime.Now;
            _context.ExternalDependent.Update(entity);
        }


        #endregion

        #region DELETE

        public void DeleteDependent(decimal id, UnitOfWork uow, decimal userId)
        {
            Dependent entity = _context.Dependent.FirstOrDefault(x => x.Id == id);

            entity.ActiveFlag = "N";
            entity.UpdRow = DateTime.Now;
            _context.Dependent.Update(entity);

            uow.LogRepository.LogDependent(entity, userId, CActions.delete);
        }
        public void DeleteExternalDependent(ExternalDependentDTO dto)
        {
            ExternalDependent entity = _context.ExternalDependent.FirstOrDefault(x => x.Number == dto.Number && x.Name == dto.Name);

            entity.ActiveFlag = "N";
            entity.UpdRow = DateTime.Now;
            _context.ExternalDependent.Update(entity);

        }

       
        #endregion

        #region ANY
        public bool ExistDependentByNumber(decimal number)
        {
            return _context.Dependent.Any(x => x.Number == number);
        }

        public bool ExistDependentById(decimal id)
        {
            return _context.Dependent.Any(x => x.Id == id);
        }

        public bool ExistDependentByNumberAndName(decimal number, string name)
        {
            return _context.ExternalDependent.Any(x => x.Number == number && x.Name == name);
        }

        #endregion

        #region GET
        public DependentCreationFrontDTO GetDependentCompleteById(int id)
        {
            var x = _context.VDependent.FirstOrDefault(x => x.Id == id);
            return _mapper.MapToEditObject(x);
        }

        public IQueryable<VDependent> GetDependents(string search)
        {
            var test = _context.VDependent.ToList();
            return _context.VDependent.AsNoTracking().Where(x => x.LastName.ToLower().Contains(search.ToLower()) || x.Number.ToString().Contains(search.ToLower())).AsQueryable();
        }

        public IQueryable<ExternalDependent> GetExternalDependents(string search)
        {
            var test = _context.ExternalDependent.ToList();
            return _context.ExternalDependent.AsNoTracking().Where(x => x.Name.ToLower().Contains(search.ToLower())
            || x.Number.ToString().Contains(search.ToLower())
            || x.Address.Contains(search.ToLower())).AsQueryable();
        }

        public List<DistanceResponseDTO> GetDependentsWithUbications()
        {
            return _context.VDependent.AsNoTracking().Where(x => x.Condition == "SubAgente")
                .Select(s => new DistanceResponseDTO { Number = s.Number, Latitude = double.Parse(s.Latitude), Longitude = double.Parse(s.Longitude) }).ToList();
        }

        public IQueryable<VExCandidateDependent> GetExCandidateDependents(string search, string filter)
        {

            var query = _context.VExCandidateDependent.AsNoTracking().Where(x => x.LastName.ToLower().Contains(search.ToLower())
            || x.PersonalDocument.Contains(search.ToLower())).AsQueryable();


            if (!string.IsNullOrEmpty(filter))
            {
                query = (IQueryable<VExCandidateDependent>)query.Where(x => x.Type.ToLower() == filter).AsQueryable();
            }

            return query;


            //return _context.VExCandidateDependent.AsNoTracking();
        }

        public int GetShopCountByNeighborhood(string? neighborhood)
        {
            return (from dep in _context.Dependent.AsNoTracking()
                    join shop in _context.ShopData on dep.Id equals shop.IdDependent
                    where dep.Condition == "SubAgente" && shop.Neighborhood == neighborhood select dep.Id).Count();
        }
        public int GetExternalShopCountByNeighborhood(string? neighborhood)
        {
            return _context.ExternalDependent.AsNoTracking().Where(x => x.Neighborhood == neighborhood).Count();
        }

        public ExternalDependentDTO GetExternalDependentByNumberAndName(decimal number, string name)
        {
            return _emapper.MapToObject(_context.ExternalDependent.Where(x=>x.Number==number && x.Name== name).FirstOrDefault());
        }
        internal ExternalDependentDTO GetExternalDependentByid(int id)
        {
            return _emapper.MapToObject(_context.ExternalDependent.Where(x => x.Id == id).FirstOrDefault());
        }

        public List<ExternalDependentDTO> GetExternalDependentsUnchanged()
        {
            return _emapper.MapToObject(_context.ExternalDependent.Where(x => x.UpdRow > (DateTime.Now.AddDays(10))).ToList());
        }

        #endregion

    }
}
