using MVP.Entities.Enums;
using System;
using MVP.Entities.Dtos.RentalCarsInformation;

namespace MVP.Entities.Entities
{
    public class RentalCarInformation
    {
        public int Id { get; private set; }
        public int TripId { get; set; }
        public virtual Trip Trip { get; set; }
        public double Cost { get; set; }
        public DateTimeOffset Start { get; set; }
        public DateTimeOffset End { get; set; }
        public RentalCarStatus Status{ get; set; }
        public string PickupAddress { get; set; }
        public string DropOffAddress { get; set; }

        public void UpdateRentalCarInformation(UpdateRentalCarInformationDto updateRentalCarInformationDto)
        {
            Cost = updateRentalCarInformationDto.Cost;
            Start = updateRentalCarInformationDto.Start;
            End = updateRentalCarInformationDto.End;
            Status = updateRentalCarInformationDto.Status;
            PickupAddress = updateRentalCarInformationDto.PickupAddress;
            DropOffAddress = updateRentalCarInformationDto.DropOffAddress;
        }
    }
}
