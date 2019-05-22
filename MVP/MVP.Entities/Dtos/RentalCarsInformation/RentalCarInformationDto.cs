﻿using MVP.Entities.Entities;
using MVP.Entities.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace MVP.Entities.Dtos.RentalCarsInformation
{
    public class RentalCarInformationDto
    {
        public virtual int Id { get; set; }

        [Required]
        public double Cost { get; set; }

        [Required]
        public DateTimeOffset Start { get; set; }

        [Required]
        public DateTimeOffset End { get; set; }

        [Required]
        public RentalCarStatus Status { get; set; }


        public static RentalCarInformationDto ToDto(RentalCarInformation rentalCarInformation)
        {
            return new RentalCarInformationDto
            {
                Id = rentalCarInformation.Id,
                Cost = rentalCarInformation.Cost,
                End = rentalCarInformation.End,
                Start = rentalCarInformation.Start,
                Status = rentalCarInformation.Status
            };
        }

        public static RentalCarInformation ToEntity(RentalCarInformationDto rentalCarInformationDto)
        {
            return new RentalCarInformation
            {
                Cost = rentalCarInformationDto.Cost,
                End = rentalCarInformationDto.End,
                Start = rentalCarInformationDto.Start,
                Status = rentalCarInformationDto.Status
            };
        }
    }
}
