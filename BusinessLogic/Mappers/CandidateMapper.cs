using BusinessLogic.DTOs.Candidate;
using BusinessLogic.DTOs.ContactPerson;
using BusinessLogic.DTOs.Dependent;
using BusinessLogic.DTOs.ShopData;
using CommonSolution.Constants;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Mappers
{
    public class CandidateMapper
    {

        public Candidate MapToEntity(CandidateCreationDTO dto)
        {
            if (dto == null)
                throw new Exception("No hay objeto para mapear");

            return new Candidate()
            {
                Number = dto.Number,
                Name = dto.Name,
                LastName = dto.LastName,
                BirthDate = dto.BirthDate,
                PersonalDocument = dto.PersonalDocument,
                Gender = dto.Gender,
                MaritalStatus = dto.MaritalStatus,
                PersonalAddress = dto.PersonalAddress,
                Phone = dto.Phone,
                Condition = dto.Condition,
                Status = dto.Status,
                IdDecisionSupport = dto.IdDecisionSupport,
            };
        }

        public CandidateDTO MapToObject(VCandidate entity)
        {
            if (entity == null)
                throw new Exception("No hay entidad para mapear");

            return new CandidateDTO()
            {
                Id = entity.Id,
                name = entity.Name,
                lastName = entity.LastName,
                birthDate = entity.BirthDate,
                personalDocument = entity.PersonalDocument,
                Gender = entity.Gender,
                MaritalStatus = entity.MaritalStatus,
                personalAddress = entity.PersonalAddress,
                phone = entity.Phone,
                status = entity.Status,
                addRow = entity.AddRow,
                number = entity.Number,
                condition = entity.Condition,
                idShopData = entity.IdShopData,
                cName = entity.NameShopData,
                cPhone = entity.PhoneShopData,
                address = entity.Address,
                neighborhood = entity.Neighborhood,
                shopType = entity.ShopType,
                latitude = entity.Latitude == null ? null : double.Parse(entity.Latitude),
                longitude = entity.Longitude == null ? null : double.Parse(entity.Longitude),
                cpName = entity.NameContactPerson,
                cpLastName = entity.LastNameContactPerson,
                cpPhone = entity.PhoneContactPerson,
                bond = entity.Bond,
                idContactPerson = entity.IdContactPerson,                
            };
        }

        public CandidateCreationFrontDTO MapToEditObject(VCandidate entity)
        {
            if (entity == null)
                throw new Exception("No hay entidad para mapear");

            return new CandidateCreationFrontDTO()
            {
                id = entity.Id,
                name = entity.Name,
                lastName = entity.LastName,
                birthDate = entity.BirthDate.ToString(),
                personalDocument = entity.PersonalDocument,
                Gender = entity.Gender,
                MaritalStatus = entity.MaritalStatus,
                personalAddress = entity.PersonalAddress,
                phone = entity.Phone,
                number = entity.Number,
                condition = entity.Condition,
                idShopData = entity.IdShopData,
                cName = entity.NameShopData,
                cPhone = entity.PhoneShopData,
                address = entity.Address,
                neighborhood = entity.Neighborhood,
                shopType = entity.ShopType,
                latitude = entity.Latitude == null ? null : double.Parse(entity.Latitude),
                longitude = entity.Longitude == null ? null : double.Parse(entity.Longitude),
                cpName = entity.NameContactPerson,
                cpLastName = entity.LastNameContactPerson,
                cpPhone = entity.PhoneContactPerson,
                bond = entity.Bond,
                idContactPerson = entity.IdContactPerson,
                idDecisionSupport = entity.IdDecisionSupport
            };
        }

        public CandidateCreationDTO MapToObject(Candidate entity)
        {
            if (entity == null)
                throw new Exception("No hay entidad para mapear");

            return new CandidateCreationDTO()
            {
                Number = entity.Number,
                Name = entity.Name,
                LastName = entity.Name,
                BirthDate = entity.BirthDate,
                PersonalDocument = entity.PersonalDocument,
                Gender = entity.Gender,
                MaritalStatus = entity.MaritalStatus,
                PersonalAddress = entity.PersonalAddress,
                Phone = entity.Phone,
                Condition = entity.Condition,
                Id = entity.Id,
                Status = entity.Status,
                IdDecisionSupport = entity.IdDecisionSupport,
            };
        }

        public List<CandidateDTO> MapToObject(List<VCandidate> colEntity)
        {
            List<CandidateDTO> colObject = new List<CandidateDTO>();

            colEntity.ForEach(x => colObject.Add(this.MapToObject(x)));

            return colObject;
        }

        public Candidate MapToEditEntity(CandidateCreationDTO dto, Candidate entity)
        {
            if (dto == null || entity == null)
                throw new Exception("No hay objeto/entidad para mapear");

            entity.Name = dto.Name;
            entity.LastName = dto.LastName;
            entity.BirthDate = dto.BirthDate;
            entity.PersonalDocument = dto.PersonalDocument;
            entity.Gender = dto.Gender;
            entity.MaritalStatus = dto.MaritalStatus;
            entity.PersonalAddress = dto.PersonalAddress;
            entity.Phone = dto.Phone;
            entity.Condition = dto.Condition;
            entity.Status = dto.Status;
            entity.Number = dto.Number;
            entity.IdDecisionSupport = dto.IdDecisionSupport;

            return entity;
        }

        public CandidateCreationDTO MapToObject(CandidateCreationFrontDTO frontDTO)
        {
            if (frontDTO == null)
                throw new Exception("No hay objeto/entidad para mapear");

            return new CandidateCreationDTO
            {
                Id = frontDTO.id ?? 0,
                Name = frontDTO.name,
                LastName = frontDTO.lastName,
                BirthDate = DateTime.Parse(frontDTO.birthDate),
                PersonalDocument = frontDTO.personalDocument,
                Gender = frontDTO.Gender,
                MaritalStatus = frontDTO.MaritalStatus,
                PersonalAddress = frontDTO.personalAddress,
                Phone = frontDTO.phone,
                Condition = frontDTO.condition,
                Status = CStatus.enrolled,
                Number = frontDTO.number,
                IdDecisionSupport = frontDTO.idDecisionSupport,

                ContactPerson = new ContactPersonCreationDTO
                {
                    Id = frontDTO.idContactPerson ?? 0,
                    Name = frontDTO.cpName,
                    LastName = frontDTO.cpLastName,
                    Phone = frontDTO.cpPhone,
                    Bond = frontDTO.bond
                },

                ShopData = string.IsNullOrEmpty(frontDTO.cName) ? null : new ShopDataCreationDTO
                {
                    Id = frontDTO.idShopData ?? 0,
                    Name = frontDTO.cName,
                    Phone = frontDTO.cPhone,
                    Address = frontDTO.address,
                    Neighborhood = frontDTO.neighborhood,
                    ShopType = frontDTO.shopType,
                    Latitude = frontDTO.latitude.ToString(),
                    Longitude = frontDTO.longitude.ToString(),
                }
            };
        }

        public ProcedureStep MapToEntity(ProcedureStepDTO dto)
        {
            if (dto == null)
                throw new Exception("No hay entidad para mapear");

            return new ProcedureStep()
            {
                StepType = dto.StepType,
                Description = dto.Description,
                IdCandidate = (decimal)dto.IdCandidate,
                UpdUser = (decimal)dto.UpdUser
            };
        }

        public ProcedureStepDTO MapToObject(ProcedureStep entity)
        {
            if (entity == null)
                throw new Exception("No hay entidad para mapear");

            return new ProcedureStepDTO()
            {
                StepType = entity.StepType,
                Description = entity.Description,
                IdCandidate = entity.IdCandidate,
                UpdUser = entity.UpdUser,
                AddRow = entity.AddRow.ToString(),
            };
        }

        public List<ProcedureStepDTO> MapToObject(List<ProcedureStep> colEntity)
        {
            List<ProcedureStepDTO> colObject = new List<ProcedureStepDTO>();

            colEntity.ForEach(x => colObject.Add(this.MapToObject(x)));

            return colObject;
        }

        public DependentCreationDTO MapToDependentObject(CandidateCreationFrontDTO candidate)
        {
            if (candidate == null)
                throw new Exception("No hay objeto/entidad para mapear");

            return new DependentCreationDTO
            {
                Id = candidate.id ?? 0,
                Name = candidate.name,
                LastName = candidate.lastName,
                BirthDate = DateTime.Parse(candidate.birthDate),
                PersonalDocument = candidate.personalDocument,
                Gender = candidate.Gender,
                MaritalStatus = candidate.MaritalStatus,
                PersonalAddress = candidate.personalAddress,
                Phone = candidate.phone,
                Condition = candidate.condition,
                Number = candidate.number ?? 0,

                ContactPerson = new ContactPersonCreationDTO
                {
                    Id = candidate.idContactPerson ?? -1,
                    Name = candidate.cpName,
                    LastName = candidate.cpLastName,
                    Phone = candidate.cpPhone,
                    Bond = candidate.bond
                },

                ShopData = string.IsNullOrEmpty(candidate.cName) ? null : new ShopDataCreationDTO
                {
                    Id = candidate.idShopData ?? -1,
                    Name = candidate.cName,
                    Phone = candidate.cPhone,
                    Address = candidate.address,
                    Neighborhood = candidate.neighborhood,
                    ShopType = candidate.shopType,
                    Latitude = candidate.latitude.ToString(),
                    Longitude = candidate.longitude.ToString(),
                }
            };

        }
    }
}
