using AutoMapper;
using BusinessLogic.DTOs.Generals;
using BusinessLogic.DTOs.ShopData;
using BusinessLogic.DTOs.User;
using BusinessLogic.Mappers;
using BusinessLogic.Utils;
using CommonSolution.Constants;
using DataAccess.Context;
using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DataModel.Repository
{
    public class ShopDataRepository
    {
        private readonly Agencia_8Context _context;
        private readonly ShopDataMapper _mapper;

        public ShopDataRepository(Agencia_8Context context)
        {
            this._context = context;
            this._mapper = new ShopDataMapper();
        }

        #region ADD

        public async void AddShopData(ShopDataCreationDTO shopData, UnitOfWork uow, decimal userId)
        {
            ShopData entity = _mapper.MapToEntity(shopData);
            entity.AddRow = DateTime.Now;

            await _context.ShopData.AddAsync(entity);
            uow.LogRepository.LogShopData(entity, userId, CActions.add);
        }

        #endregion

        #region UPDATE

        public void UpdateShopData(ShopDataCreationDTO shopData, UnitOfWork uow, decimal userId)
        {
            ShopData entity = this.GetShopDataById(shopData.Id);

            entity = _mapper.MapToEditEntity(shopData, entity);
            entity.UpdRow = DateTime.Now;

            _context.ShopData.Update(entity);
            uow.LogRepository.LogShopData(entity, userId, CActions.edit);
        }

        #endregion

        #region DELETE


        public void DeleteShopData(decimal number, UnitOfWork uow, decimal userId)
        {
            ShopData entity = this.GetShopDataById(number);

            _context.ShopData.Remove(entity);
            uow.LogRepository.LogShopData(entity, userId, CActions.delete);
        }

        #endregion

        #region ANY

        public bool ExistShopDataByNumber(decimal number)
        {
            return _context.ShopData.Any(x => x.Id == number);
        }

        #endregion

        #region GET


        public ShopData GetShopDataById(decimal id)
        {
            return _context.ShopData.FirstOrDefault(x => x.Id == id);
        }

        public ShopData GetShopDataByIdCandidate(decimal id)
        {
            return _context.ShopData.FirstOrDefault(x => x.IdCandidate == id);
        }

        #endregion

    }
}
