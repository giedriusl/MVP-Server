using MVP.Entities.Entities;
using MVP.Entities.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace MVP.Entities.Dtos.RentalCarsInformation
{
    public class RentalCarInformationDto
    {
        public virtual int Id { get; set; }
        public double Cost { get; set; }
        public DateTimeOffset Start { get; set; }
        public DateTimeOffset End { get; set; }
        public string PickupAddress { get; set; }
        public string DropOffAddress { get; set; }

        [Required]
        public RentalCarStatus Status { get; set; }
        public string StatusName { get; set; }

        public static RentalCarInformationDto ToDto(RentalCarInformation rentalCarInformation)
        {
            return new RentalCarInformationDto
            {
                Id = rentalCarInformation.Id,
                Cost = rentalCarInformation.Cost,
                End = rentalCarInformation.End,
                Start = rentalCarInformation.Start,
                Status = rentalCarInformation.Status,
                StatusName = rentalCarInformation.Status.ToString(),
                PickupAddress = rentalCarInformation.PickupAddress,
                DropOffAddress = rentalCarInformation.DropOffAddress
            };
        }

        public static RentalCarInformation ToEntity(RentalCarInformationDto rentalCarInformationDto)
        {
            return new RentalCarInformation
            {
                Cost = rentalCarInformationDto.Cost,
                End = rentalCarInformationDto.End,
                Start = rentalCarInformationDto.Start,
                Status = rentalCarInformationDto.Status,
                PickupAddress = rentalCarInformationDto.PickupAddress,
                DropOffAddress = rentalCarInformationDto.DropOffAddress
            };
        }
    }
}
