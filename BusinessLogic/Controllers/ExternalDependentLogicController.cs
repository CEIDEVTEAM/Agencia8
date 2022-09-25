using BusinessLogic.DataModel;
using BusinessLogic.DTOs.Dependent;
using BusinessLogic.DTOs.Generals;
using BusinessLogic.Utils;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Controllers
{
    public class ExternalDependentLogicController
    {
        private IConfiguration _configuration;
        private string _application;

        public ExternalDependentLogicController(IConfiguration configuration, string application)
        {
            this._configuration = configuration;
            this._application = application;
        }


        public async Task<GenericResponse> ProccessExternalDependets()
        {
            List<string> errors = new List<string>();
            bool successful = false;
            List<ExternalDependentDTO> externalDependentDTOs = DnlqWebCrawling.ProcessDNLQData();


            using (var uow = new UnitOfWork(_configuration, _application))
            {
                uow.BeginTransaction();

                try
                {
                    foreach (var item in externalDependentDTOs)
                    {
                        bool existExternal = uow.DependentRepository.ExistDependentByNumberAndName(item.Number, item.Name);
                        if (existExternal)
                        {
                            ExternalDependentDTO extDto = uow.DependentRepository.GetExternalDependentByNumberAndName(item.Number, item.Name);
                            extDto.Name = item.Name;
                            extDto.Number = item.Number;
                            extDto.Address = item.Address;
                            uow.DependentRepository.UpdateExternalDependent(extDto);
                        }
                        else
                        {
                            uow.DependentRepository.AddExternalDependent(item);
                        }
                    }
                    uow.SaveChanges();
                    uow.Commit();
                    successful = true;
                }
                catch (Exception ex)
                {
                    errors.Add("Error al comunicarse con la base de datos");
                    uow.Rollback();
                }
            }

            return new GenericResponse()
            {
                Errors = errors,
                Successful = successful
            };

        }

        public ActionResult<ExternalDependentDTO> GetExternalDependentByKey(string id)
        {
            using (var uow = new UnitOfWork(_configuration, _application))
            {
                decimal number = decimal.Parse(id.Split("-")[0]);
                string name = id.Split("-")[1];

                ExternalDependentDTO dto = uow.DependentRepository.GetExternalDependentByNumberAndName(number, name);
                return dto;

            }
        }

        public ActionResult<GenericResponse> EditExternalDependent(ExternalDependentDTO dto)
        {
            List<string> errors = new List<string>();
            bool successful = false;

            using (var uow = new UnitOfWork(_configuration, _application))
            {
                uow.BeginTransaction();

                try
                {
                    uow.DependentRepository.UpdateExternalDependent(dto);

                    uow.SaveChanges();
                    uow.Commit();
                    successful = true;

                }
                catch (Exception ex)
                {
                    errors.Add("Error al comunicarse con la base de datos");
                    uow.Rollback();
                }
            }

            return new GenericResponse()
            {
                Errors = errors,
                Successful = successful
            };
        }


    }






}
