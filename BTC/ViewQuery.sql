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
select RFQ.ID RFQID,
CASE RFQ.Processing
WHEN 0 THEN 'Not Processed'
WHEN 1 THEN 'Being Processed'
WHEN 2 THEN 'Processed'
END Processing,
 case RFQ.ProcessingSection
	WHEN 1 THEN 'AT'
	WHEN 2 THEN 'HS'
	WHEN 3 THEN 'PC'
	WHEN 4 THEN 'AT + HS'
	WHEN 5 THEN 'AT + PC'
	WHEN 6 THEN 'HS + PC'
	WHEN 7 THEN 'AT + HS + PC'
END ProcessingSection,
RFQ.Remarks RFQRemarks, ta.AgencyCode,u.FirstName + u.LastName username,tr.* FROM RFQ
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

Create view LPODetails AS (
select LPO.ID LPOID,LPO.Remarks LPORemarks,LPO.isAT, LPO.IsHS,LPO.IsPC,RFQDet.*,
ATQuotation.Airlines,ATQuotation.Amount ATAmount,ATQuotation.DepartureDate ATDepartureDate,ATQuotation.DepartureTime ATDepartureTime,ATQuotation.DestinationID,atd.CityDesc ATDCity,atd.Country ATDCountry,ATQuotation.OriginID,ato.CityDesc ATOCity,ato.Country ATOCountry, ATQuotation.QuotationName ATQuotationName,ATQuotation.ReturnDate ATReturnDate,ATQuotation.ReturnTime ATReturnTime,ATQuotation.TicketClass ATTicketClass,ATQuotation.TicketNo,
HSQuotation.Amount HSAmount,HSQuotation.CheckInDate HSCheckInDate,HSQuotation.CheckInTime HSCheckInTime ,HSQuotation.CheckOutDate HSCheckOutDate,HSQuotation.CheckOutTime HSCheckOutTime,HSQuotation.HotelCategory HSHotelCategory,HSQuotation.HotelName HSHotelName,HSQuotation.QuotationName HSQuotationName,HSQuotation.RoomCategory HSRoomCategory,HSQuotation.RoomType HSRoomType,HSQuotation.TravelSector HSTravelSector,
PCQuotation.Amount PCAmount,PCQuotation.DropOffDate PCDropOffDate,PCQuotation.DropoffLocation PCDropoffLocation,PCQuotation.DropOffTime PCDropOffTime,PCQuotation.PickUpDate PCPickUpDate,PCQuotation.PickupLocation PCPickupLocation,PCQuotation.PickUpTime PCPickUpTime,PCQuotation.PreferredVehicle PCPreferredVehicle,PCQuotation.QuotationName PCQuotationName,PCQuotation.TravelSector PCTravelSector
FROM LPO
left join RFQDetails RFQdet on LPO.RFQID = RFQdet.RFQID
left join Quotation on LPO.QuotationID = Quotation.ID
left join ATQuotation on ATQuotation.QuotationID = Quotation.ID
left join HSQuotation on HSQuotation.QuotationID = Quotation.ID
left join PCQuotation on PCQuotation.QuotationID = Quotation.ID
left join City Ato on ato.ID = ATQuotation.OriginID
left join City Atd on atd.ID = ATQuotation.DestinationID
)
