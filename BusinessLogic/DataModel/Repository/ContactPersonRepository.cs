using AutoMapper;
using BusinessLogic.DTOs.ContactPerson;
using BusinessLogic.DTOs.Dependent;
using BusinessLogic.DTOs.Generals;
using BusinessLogic.DTOs.User;
using BusinessLogic.Mappers;
using BusinessLogic.Utils;
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
    public class ContactPersonRepository
    {
        private readonly Agencia_8Context _context;
        private readonly ContactPersonMapper _mapper;

        public ContactPersonRepository(Agencia_8Context context)
        {
            this._context = context;
            this._mapper = new ContactPersonMapper();
        }

        #region ADD

        public async void AddContactPerson(ContactPersonCreationDTO contactPerson)
        {
            ContactPerson entity = _mapper.MapToEntity(contactPerson);
            entity.AddRow = DateTime.Now;

            await _context.ContactPerson.AddAsync(entity);
        }

        #endregion

        #region UPDATE

        public void UpdateContactPerson(ContactPersonCreationDTO contactPerson)
        {
            ContactPerson entity = this.GetContactPersonByNumber(contactPerson.Number);
            entity = _mapper.MapToEditEntity(contactPerson, entity);

            entity.UpdRow = DateTime.Now;
            _context.ContactPerson.Update(entity);
        }

        #endregion

        #region DELETE

        public void DeleteContactPerson(decimal number)
        {
            ContactPerson entity = this.GetContactPersonByNumber(number);

            _context.ContactPerson.Remove(entity);
        }

        #endregion

        #region ANY

        public bool ExistContactPersonByNumber(decimal number)
        {
            return _context.ContactPerson.Any(x => x.Number == number);
        }

        #endregion

        #region GET

        public ContactPerson GetContactPersonByNumber(decimal number)
        {
            return _context.ContactPerson.FirstOrDefault(x => x.Number == number);
        }

        #endregion

    }
}
