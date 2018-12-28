create view TravelRequestDetails AS (
select TravelRequests.ID,ApplicationNumber,PortOfOriginID,co.CityDesc OriginCIty,co.Country OriginCountry,PortOfDestinationID,cd.CityDesc DestinationCity,cd.Country DestinationCountry, TicketClass,DailyAllowance,CurrencyID,cu.CurrencyDesc,cu.CurrencySymbol,TravelDays,TravelRemarks,PurposeOfVisit,DepartureDate,DepartureTime,ReturnDate,ReturnTime,FirstBusinessDay,LastBusinessDay,Remarks,AirTicketManagement,HotelName,TravelAllowance,HotelStay,HotelCategory,RoomCategory,RoomType,AdditionalAllowance,AirportPickUp,PickUpLocation,PickUpDate,PickUpTime,DropOffLocation,DropOffDate,DropOffTime,PreferredVehicle,TravelSector,CheckInDate,CheckOutDate,CheckInTime,CheckOutTime,ApprovalLevel,ApprovalBy,uap.FirstName + uap.LastName ApprovedByName,ApprovalRemarks,CreateOn,CreatedBy,ucb.FirstName + ucb.LastName CreatedByName,ModifiedOn,ModifiedBy,umb.FirstName + umb.LastName ModifiedByName,TravelRequests.IsDeleted,IsSubmitted
FROM TravelRequests
left join City co on co.ID = TravelRequests.PortOfOriginID
left join City cd on cd.ID = TravelRequests.PortOfDestinationID
left join Currency cu on cu.ID = TravelRequests.CurrencyID
left join Users uap on uap.ID = TravelRequests.ApprovalBy
left join Users ucb on ucb.ID = TravelRequests.CreatedBy
left join Users umb on umb.ID = TravelRequests.ModifiedBy
)

create view RFQDetails AS (
select RFQ.ID RFQID,RFQ.Processing,RFQ.ProcessingSection,RFQ.Remarks RFQRemarks, ta.AgencyCode,u.FirstName + u.LastName username,tr.* FROM RFQ
inner join (select TravelRequests.ID,ApplicationNumber,PortOfOriginID,co.CityDesc OriginCIty,co.Country OriginCountry,PortOfDestinationID,cd.CityDesc DestinationCity,cd.Country DestinationCountry, TicketClass,DailyAllowance,CurrencyID,cu.CurrencyDesc,cu.CurrencySymbol,TravelDays,TravelRemarks,PurposeOfVisit,DepartureDate,DepartureTime,ReturnDate,ReturnTime,FirstBusinessDay,LastBusinessDay,Remarks,AirTicketManagement,HotelName,TravelAllowance,HotelStay,HotelCategory,RoomCategory,RoomType,AdditionalAllowance,AirportPickUp,PickUpLocation,PickUpDate,PickUpTime,DropOffLocation,DropOffDate,DropOffTime,PreferredVehicle,TravelSector,CheckInDate,CheckOutDate,CheckInTime,CheckOutTime,ApprovalLevel,ApprovalBy,uap.FirstName + uap.LastName ApprovedByName,ApprovalRemarks,CreateOn,CreatedBy,ucb.FirstName + ucb.LastName CreatedByName,ModifiedOn,ModifiedBy,umb.FirstName + umb.LastName ModifiedByName,TravelRequests.IsDeleted,IsSubmitted
FROM TravelRequests
left join City co on co.ID = TravelRequests.PortOfOriginID
left join City cd on cd.ID = TravelRequests.PortOfDestinationID
left join Currency cu on cu.ID = TravelRequests.CurrencyID
left join Users uap on uap.ID = TravelRequests.ApprovalBy
left join Users ucb on ucb.ID = TravelRequests.CreatedBy
left join Users umb on umb.ID = TravelRequests.ModifiedBy
) tr on tr.ID = RFQ.TravelRequestID
left join TravelAgency ta on ta.ID = rfq.TravelAgencyID
left join Users u on u.ID = RFQ.UserID
)