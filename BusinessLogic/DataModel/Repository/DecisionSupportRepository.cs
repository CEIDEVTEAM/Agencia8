using BusinessLogic.DTOs.Candidate;
using BusinessLogic.DTOs.DecisionParam;
using BusinessLogic.DTOs.Dependent;
using BusinessLogic.Mappers;
using CommonSolution.Constants;
using DataAccess.Context;
using DataAccess.Models;

namespace BusinessLogic.DataModel.Repository
{
    public class DecisionSupportRepository
    {
        private readonly Agencia_8Context _context;
        private readonly DecisionSupportMapper _mapper;

        public DecisionSupportRepository(Agencia_8Context context)
        {
            this._context = context;
            this._mapper = new DecisionSupportMapper();
        }

        #region ADD
        public decimal AddDecision(DecisionSupportDTO obj)
        {
            DecisionSupport entity = _mapper.MapToEntity(obj);
            entity.AddRow = DateTime.Now;
            _context.DecisionSupport.AddAsync(entity);
            return entity.Id;
            
        }

        #endregion

        #region UPDATE



        #endregion

        #region DELETE


        #endregion

        #region ANY
        public bool ExistDecisionParamById(decimal id)
        {
            return _context.DecisionParam.Any(x => x.Id == id);
        }

        #endregion

        #region GET


        public DecisionSupportDTO GetMappedDecisionParamById(decimal id)
        {
            return this._mapper.MapToObject(_context.DecisionSupport.FirstOrDefault(x => x.Id == id));
        }



        #endregion

    }
}
