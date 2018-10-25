using System;
using System.Collections.Generic;

namespace BTC_Web_API.Models
{

    public class HRW_Employee
    {
        private int _CustomerID;
        private string _FirstName;
        private string _MiddleName;
        private string _LastName;
        private string _Email;
        private DateTime? _DOB;
        private bool _Gender;
        private bool _IsActive;
        private string _Mobile;
        private string _Address;
        private string _City;
        private string _Country;
        private string _Pincode;
        private string _Password;
        private string _ConfirmPassword;
        private int _TotalOrders;
        private int _RoleID;
        public List<HRW_EmpEntityParamValues> _Orders;

        public int CustomerID
        {
            get { return _CustomerID; }
            set { _CustomerID = value; }
        }
        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        public string MiddleName
        {
            get { return _MiddleName; }
            set { _MiddleName = value; }
        }
        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
        public DateTime? DOB
        {
            get { return _DOB; }
            set { _DOB = value; }
        }
        public bool Gender
        {
            get { return _Gender; }
            set { _Gender = value; }
        }
        public bool IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }
        public string Mobile
        {
            get { return _Mobile; }
            set { _Mobile = value; }
        }
        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }
        public string City
        {
            get { return _City; }
            set { _City = value; }
        }
        public string Country
        {
            get { return _Country; }
            set { _Country = value; }
        }
        public string Pincode
        {
            get { return _Pincode; }
            set { _Pincode = value; }
        }
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }
        public string ConfirmPassword
        {
            get { return _ConfirmPassword; }
            set { _ConfirmPassword = value; }
        }
        public int TotalOrders
        {
            get { return _TotalOrders; }
            set { _TotalOrders = value; }
        }
        public int RoleID
        {
            get { return _RoleID; }
            set { _RoleID = value; }
        }
    }

    public class HRW_EmpEntityParamValues
    {
        private int _OrderID;

        public int OrderID
        {
            get { return _OrderID; }
            set { _OrderID = value; }
        }

        private string _OrderNumber;

        public string OrderNumber
        {
            get { return _OrderNumber; }
            set { _OrderNumber = value; }
        }

        private int _CustomerID;

        public int CustomerID
        {
            get { return _CustomerID; }
            set { _CustomerID = value; }
        }

        private bool _IsNew;

        public bool IsNew
        {
            get { return _IsNew; }
            set { _IsNew = value; }
        }

        private string _Fault;

        public string Fault
        {
            get { return _Fault; }
            set { _Fault = value; }
        }

        private bool _Evidence;

        public bool Evidence
        {
            get { return _Evidence; }
            set { _Evidence = value; }
        }

        private string _Company;

        public string Company
        {
            get { return _Company; }
            set { _Company = value; }
        }

        private string _Reference;

        public string Reference
        {
            get { return _Reference; }
            set { _Reference = value; }
        }

        private int _OrderTypeID;

        public int OrderTypeID
        {
            get { return _OrderTypeID; }
            set { _OrderTypeID = value; }
        }

        private string _OrderTypeName;

        public string OrderTypeName
        {
            get { return _OrderTypeName; }
            set { _OrderTypeName = value; }
        }

        private int _OrderStatusID;

        public int OrderStatusID
        {
            get { return _OrderStatusID; }
            set { _OrderStatusID = value; }
        }

        private string _OrderStatusName;

        public string OrderStatusName
        {
            get { return _OrderStatusName; }
            set { _OrderStatusName = value; }
        }


        private DateTime _OrderDate;

        public DateTime OrderDate
        {
            get { return _OrderDate; }
            set { _OrderDate = value; }
        }

        private int _NumbOfBlinds;

        public int NumbOfBlinds
        {
            get { return _NumbOfBlinds; }
            set { _NumbOfBlinds = value; }
        }

        private string _ConsignNoteNum;

        public string ConsignNoteNum
        {
            get { return _ConsignNoteNum; }
            set { _ConsignNoteNum = value; }
        }

        private DateTime? _CompleteDate;

        public DateTime? CompleteDate
        {
            get { return _CompleteDate; }
            set { _CompleteDate = value; }
        }

        private DateTime? _DeliveryDate;

        public DateTime? DeliveryDate
        {
            get { return _DeliveryDate; }
            set { _DeliveryDate = value; }
        }

        private DateTime? _DepartureDate;

        public DateTime? DepartureDate
        {
            get { return _DepartureDate; }
            set { _DepartureDate = value; }
        }

        private DateTime? _ArrivalDate;

        public DateTime? ArrivalDate
        {
            get { return _ArrivalDate; }
            set { _ArrivalDate = value; }
        }

        private int _BlindTypeID;

        public int BlindTypeID
        {
            get { return _BlindTypeID; }
            set { _BlindTypeID = value; }
        }

        private string _BlindTypeName;

        public string BlindTypeName
        {
            get { return _BlindTypeName; }
            set { _BlindTypeName = value; }
        }

        private string _Transport;

        public string Transport
        {
            get { return _Transport; }
            set { _Transport = value; }
        }


        private string _Notes;

        public string Notes
        {
            get { return _Notes; }
            set { _Notes = value; }
        }

        private double _OrderM2;

        public double OrderM2
        {
            get { return _OrderM2; }
            set { _OrderM2 = value; }
        }

        private bool _IsApproved;

        public bool IsApproved
        {
            get { return _IsApproved; }
            set { _IsApproved = value; }
        }
    }

    public class ORG_EmpEntityLink
    {
        private int _OrderDetailID;

        public int OrderDetailID
        {
            get { return _OrderDetailID; }
            set { _OrderDetailID = value; }
        }

        private int _OrderID;

        public int OrderID
        {
            get { return _OrderID; }
            set { _OrderID = value; }
        }

        private double _Width;

        public double Width
        {
            get { return _Width; }
            set { _Width = value; }
        }

        private double _Height;

        public double Height
        {
            get { return _Height; }
            set { _Height = value; }
        }

        private double _SplPelmetWidth;

        public double SplPelmetWidth
        {
            get { return _SplPelmetWidth; }
            set { _SplPelmetWidth = value; }
        }

        private string _WidthMadeBy;

        public string WidthMadeBy
        {
            get { return _WidthMadeBy; }
            set { _WidthMadeBy = value; }
        }

        private string _HeightMadeBy;

        public string HeightMadeBy
        {
            get { return _HeightMadeBy; }
            set { _HeightMadeBy = value; }
        }

        private string _QualityCheckedBy;

        public string QualityCheckedBy
        {
            get { return _QualityCheckedBy; }
            set { _QualityCheckedBy = value; }
        }

        private int _SlatStyleID;

        public int SlatStyleID
        {
            get { return _SlatStyleID; }
            set { _SlatStyleID = value; }
        }

        private string _SlatStyleName;

        public string SlatStyleName
        {
            get { return _SlatStyleName; }
            set { _SlatStyleName = value; }
        }

        private int _CordStyleID;

        public int CordStyleID
        {
            get { return _CordStyleID; }
            set { _CordStyleID = value; }
        }

        private string _CordStyleName;

        public string CordStyleName
        {
            get { return _CordStyleName; }
            set { _CordStyleName = value; }
        }

        private bool _ReturnRequired;

        public bool ReturnRequired
        {
            get { return _ReturnRequired; }
            set { _ReturnRequired = value; }
        }

        private string _MountType;

        public string MountType
        {
            get { return _MountType; }
            set { _MountType = value; }
        }

        private double _SquareMeter;

        public double SquareMeter
        {
            get { return _SquareMeter; }
            set { _SquareMeter = value; }
        }

        private int _ControlID;

        public int ControlID
        {
            get { return _ControlID; }
            set { _ControlID = value; }
        }

        private string _ControlName;

        public string ControlName
        {
            get { return _ControlName; }
            set { _ControlName = value; }
        }

        private string _ControlStyle;

        public string ControlStyle
        {
            get { return _ControlStyle; }
            set { _ControlStyle = value; }
        }

        private string _OpeningStyle;

        public string OpeningStyle
        {
            get { return _OpeningStyle; }
            set { _OpeningStyle = value; }
        }

        private string _PelmetStyle;

        public string PelmetStyle
        {
            get { return _PelmetStyle; }
            set { _PelmetStyle = value; }
        }

        private int _ColorID;

        public int ColorID
        {
            get { return _ColorID; }
            set { _ColorID = value; }
        }

        private string _ColorName;

        public string ColorName
        {
            get { return _ColorName; }
            set { _ColorName = value; }
        }

        private int _MaterialID;

        public int MaterialID
        {
            get { return _MaterialID; }
            set { _MaterialID = value; }
        }

        private string _MaterialName;

        public string MaterialName
        {
            get { return _MaterialName; }
            set { _MaterialName = value; }
        }

        private string _Roll;

        public string Roll
        {
            get { return _Roll; }
            set { _Roll = value; }
        }

        private double _ReadyMadeSize;

        public double ReadyMadeSize
        {
            get { return _ReadyMadeSize; }
            set { _ReadyMadeSize = value; }
        }
    }

    public class PRL_PayrollSheet06
    {
        private int _FabricID;

        public int FabricID
        {
            get { return _FabricID; }
            set { _FabricID = value; }
        }

        private int _UtilityOrderID;

        public int UtilityOrderID
        {
            get { return _UtilityOrderID; }
            set { _UtilityOrderID = value; }
        }

        private string _FabricType;

        public string FabricType
        {
            get { return _FabricType; }
            set { _FabricType = value; }
        }

        private int _ColorID;

        public int ColorID
        {
            get { return _ColorID; }
            set { _ColorID = value; }
        }

        private string _ColorName;

        public string ColorName
        {
            get { return _ColorName; }
            set { _ColorName = value; }
        }

        private int _SizeID;

        public int SizeID
        {
            get { return _SizeID; }
            set { _SizeID = value; }
        }

        private string _SizeValue;

        public string SizeValue
        {
            get { return _SizeValue; }
            set { _SizeValue = value; }
        }

        private int _Boxes;

        public int Boxes
        {
            get { return _Boxes; }
            set { _Boxes = value; }
        }
    }

    public class ORG_EntityMaster
    {
        private int _RollerBlindTypeID;

        public int RollerBlindTypeID
        {
            get { return _RollerBlindTypeID; }
            set { _RollerBlindTypeID = value; }
        }

        private string _Description;

        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        private string _Profile;

        public string Profile
        {
            get { return _Profile; }
            set { _Profile = value; }
        }

        private string _RollerColor;

        public string RollerColor
        {
            get { return _RollerColor; }
            set { _RollerColor = value; }
        }

        private string _DLXCODE;

        public string DLXCODE
        {
            get { return _DLXCODE; }
            set { _DLXCODE = value; }
        }

        private string _PCSCTN;

        public string PCSCTN
        {
            get { return _PCSCTN; }
            set { _PCSCTN = value; }
        }

        private string _MOQ;

        public string MOQ
        {
            get { return _MOQ; }
            set { _MOQ = value; }
        }
    }

    public class ORG_TravelRequest
    {
        private int _RollerBlindTypeID;

        public int RollerBlindTypeID
        {
            get { return _RollerBlindTypeID; }
            set { _RollerBlindTypeID = value; }
        }

        private string _Description;

        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        private string _Profile;

        public string Profile
        {
            get { return _Profile; }
            set { _Profile = value; }
        }

        private string _RollerColor;

        public string RollerColor
        {
            get { return _RollerColor; }
            set { _RollerColor = value; }
        }

        private string _DLXCODE;

        public string DLXCODE
        {
            get { return _DLXCODE; }
            set { _DLXCODE = value; }
        }

        private string _PCSCTN;

        public string PCSCTN
        {
            get { return _PCSCTN; }
            set { _PCSCTN = value; }
        }

        private string _MOQ;

        public string MOQ
        {
            get { return _MOQ; }
            set { _MOQ = value; }
        }
    }

    public class ORG_Quote
    {
        private int _RollerBlindTypeID;

        public int RollerBlindTypeID
        {
            get { return _RollerBlindTypeID; }
            set { _RollerBlindTypeID = value; }
        }

        private string _Description;

        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        private string _Profile;

        public string Profile
        {
            get { return _Profile; }
            set { _Profile = value; }
        }

        private string _RollerColor;

        public string RollerColor
        {
            get { return _RollerColor; }
            set { _RollerColor = value; }
        }

        private string _DLXCODE;

        public string DLXCODE
        {
            get { return _DLXCODE; }
            set { _DLXCODE = value; }
        }

        private string _PCSCTN;

        public string PCSCTN
        {
            get { return _PCSCTN; }
            set { _PCSCTN = value; }
        }

        private string _MOQ;

        public string MOQ
        {
            get { return _MOQ; }
            set { _MOQ = value; }
        }
    }

    public class ORG_LPO
    {
        private int _RollerBlindTypeID;

        public int RollerBlindTypeID
        {
            get { return _RollerBlindTypeID; }
            set { _RollerBlindTypeID = value; }
        }

        private string _Description;

        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        private string _Profile;

        public string Profile
        {
            get { return _Profile; }
            set { _Profile = value; }
        }

        private string _RollerColor;

        public string RollerColor
        {
            get { return _RollerColor; }
            set { _RollerColor = value; }
        }

        private string _DLXCODE;

        public string DLXCODE
        {
            get { return _DLXCODE; }
            set { _DLXCODE = value; }
        }

        private string _PCSCTN;

        public string PCSCTN
        {
            get { return _PCSCTN; }
            set { _PCSCTN = value; }
        }

        private string _MOQ;

        public string MOQ
        {
            get { return _MOQ; }
            set { _MOQ = value; }
        }
    }

    public class HRW_Notification
    {
        private int _RollerBlindTypeID;

        public int RollerBlindTypeID
        {
            get { return _RollerBlindTypeID; }
            set { _RollerBlindTypeID = value; }
        }

        private string _Description;

        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        private string _Profile;

        public string Profile
        {
            get { return _Profile; }
            set { _Profile = value; }
        }

        private string _RollerColor;

        public string RollerColor
        {
            get { return _RollerColor; }
            set { _RollerColor = value; }
        }

        private string _DLXCODE;

        public string DLXCODE
        {
            get { return _DLXCODE; }
            set { _DLXCODE = value; }
        }

        private string _PCSCTN;

        public string PCSCTN
        {
            get { return _PCSCTN; }
            set { _PCSCTN = value; }
        }

        private string _MOQ;

        public string MOQ
        {
            get { return _MOQ; }
            set { _MOQ = value; }
        }
    }

    public class ORG_TravelAgency
    {
        private int _RollerBlindTypeID;

        public int RollerBlindTypeID
        {
            get { return _RollerBlindTypeID; }
            set { _RollerBlindTypeID = value; }
        }

        private string _Description;

        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        private string _Profile;

        public string Profile
        {
            get { return _Profile; }
            set { _Profile = value; }
        }

        private string _RollerColor;

        public string RollerColor
        {
            get { return _RollerColor; }
            set { _RollerColor = value; }
        }

        private string _DLXCODE;

        public string DLXCODE
        {
            get { return _DLXCODE; }
            set { _DLXCODE = value; }
        }

        private string _PCSCTN;

        public string PCSCTN
        {
            get { return _PCSCTN; }
            set { _PCSCTN = value; }
        }

        private string _MOQ;

        public string MOQ
        {
            get { return _MOQ; }
            set { _MOQ = value; }
        }
    }

    public class Destinations
    {
        private int _DestinationsID;

        public int DestinationsID
        {
            get { return _DestinationsID; }
            set { _DestinationsID = value; }
        }

        private string _DestinationsDesc;

        public string DestinationsDesc
        {
            get { return _DestinationsDesc; }
            set { _DestinationsDesc = value; }
        }

        private string _For;

        public string For
        {
            get { return _For; }
            set { _For = value; }
        }
    }

    public class HotelCategory
    {
        private int _HotelCategoryID;

        public int HotelCategoryID
        {
            get { return _HotelCategoryID; }
            set { _HotelCategoryID = value; }
        }

        private string _HotelCategoryDesc;

        public string HotelCategoryDesc
        {
            get { return _HotelCategoryDesc; }
            set { _HotelCategoryDesc = value; }
        }

        private string _For;

        public string For
        {
            get { return _For; }
            set { _For = value; }
        }
    }

    public class RoomType
    {
        private int _RoomTypeID;

        public int RoomTypeID
        {
            get { return _RoomTypeID; }
            set { _RoomTypeID = value; }
        }

        private string _RoomTypeDesc;

        public string RoomTypeDesc
        {
            get { return _RoomTypeDesc; }
            set { _RoomTypeDesc = value; }
        }

        private string _For;

        public string For
        {
            get { return _For; }
            set { _For = value; }
        }
    }
}