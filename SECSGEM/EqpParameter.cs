using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SECSGEM
{
    internal class SMT_EqpParameter
    {
        #region EqpParameter

        private static string eqpStatus = "";

        public static string EqpStatus
        {
            get { return eqpStatus; }
            set
            {
                eqpStatus = value;
            }
        }

        private static string pass1MagazineBoatCount = "";

        public static string Pass1MagazineBoatCount
        {
            get { return pass1MagazineBoatCount; }
            set
            {
                pass1MagazineBoatCount = value;
            }
        }

        private static string pass2MagazineBoatCount = "";

        public static string Pass2MagazineBoatCount
        {
            get { return pass2MagazineBoatCount; }
            set
            {
                pass2MagazineBoatCount = value;
            }
        }

        private static string nGMagazineBoatCount = "";

        public static string NGMagazineBoatCount
        {
            get { return nGMagazineBoatCount; }
            set
            {
                nGMagazineBoatCount = value;
            }
        }

        private static string emptyMagazineBoatCount = "";

        public static string EmptyMagazineBoatCount
        {
            get { return emptyMagazineBoatCount; }
            set
            {
                emptyMagazineBoatCount = value;
            }
        }

        #endregion EqpParameter

        #region SVID

        private static string magazineIDCheck = "";

        public static string MagazineIDCheck
        {
            get { return magazineIDCheck; }
            set
            {
                if (value != magazineIDCheck)
                {
                    magazineIDCheck = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(SMT_EqpID.MagazineIDCheck, value);
                }
            }
        }

        private static string boatBarcodeCheck = "";

        public static string BoatBarcodeCheck
        {
            get { return boatBarcodeCheck; }
            set
            {
                if (value != boatBarcodeCheck)
                {
                    boatBarcodeCheck = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(SMT_EqpID.BoatBarcodeCheck, value);
                }
            }
        }

        private static string pass1MagazineIDCheck = "";

        public static string Pass1MagazineIDCheck
        {
            get { return pass1MagazineIDCheck; }
            set
            {
                if (value != pass1MagazineIDCheck)
                {
                    pass1MagazineIDCheck = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(SMT_EqpID.Pass1MagazineIDCheck, value);
                }
            }
        }

        private static string pass2MagazineIDCheck = "";

        public static string Pass2MagazineIDCheck
        {
            get { return pass2MagazineIDCheck; }
            set
            {
                if (value != pass2MagazineIDCheck)
                {
                    pass2MagazineIDCheck = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(SMT_EqpID.Pass2MagazineIDCheck, value);
                }
            }
        }

        private static string nGMagazineIDCheck = "";

        public static string NGMagazineIDCheck
        {
            get { return nGMagazineIDCheck; }
            set
            {
                if (value != nGMagazineIDCheck)
                {
                    nGMagazineIDCheck = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(SMT_EqpID.NGMagazineIDCheck, value);
                }
            }
        }

        private static string emptyMagazineIDCheck = "";

        public static string EmptyMagazineIDCheck
        {
            get { return emptyMagazineIDCheck; }
            set
            {
                if (value != emptyMagazineIDCheck)
                {
                    emptyMagazineIDCheck = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(SMT_EqpID.EmptyMagazineIDCheck, value);
                }
            }
        }

        private static string boatAssingNGBoatBarcodeCheck = "";

        public static string BoatAssingNGBoatBarcodeCheck
        {
            get { return boatAssingNGBoatBarcodeCheck; }
            set
            {
                if (value != boatAssingNGBoatBarcodeCheck)
                {
                    boatAssingNGBoatBarcodeCheck = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(SMT_EqpID.BoatAssingNGBoatBarcodeCheck, value);
                }
            }
        }

        private static string uILockCommand = "";

        public static string UILockCommand
        {
            get { return uILockCommand; }
            set
            {
                if (value != uILockCommand)
                {
                    uILockCommand = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(SMT_EqpID.UILockCommand, value);
                }
            }
        }

        private static string equipmentOperationalCommand = "";

        public static string EquipmentOperationalCommand
        {
            get { return equipmentOperationalCommand; }
            set
            {
                if (value != equipmentOperationalCommand)
                {
                    equipmentOperationalCommand = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(SMT_EqpID.EquipmentOperationalCommand, value);
                }
            }
        }

        private static string equipmentEndLotCommand = "";

        public static string EquipmentEndLotCommand
        {
            get { return equipmentEndLotCommand; }
            set
            {
                if (value != equipmentEndLotCommand)
                {
                    equipmentEndLotCommand = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(SMT_EqpID.EquipmentEndLotCommand, value);
                }
            }
        }

        private static string remoteActionResult = "";

        public static string RemoteActionResult
        {
            get { return remoteActionResult; }
            set
            {
                if (value != remoteActionResult)
                {
                    remoteActionResult = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(SMT_EqpID.RemoteActionResult, value);
                }
            }
        }

        private static string emptyBoatReleaseCheck = "";

        public static string EmptyBoatReleaseCheck
        {
            get { return emptyBoatReleaseCheck; }
            set
            {
                if (value != emptyBoatReleaseCheck)
                {
                    emptyBoatReleaseCheck = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(SMT_EqpID.EmptyBoatReleaseCheck, value);
                }
            }
        }

        private static string equipmentStartRunCheck = "";

        public static string EquipmentStartRunCheck
        {
            get { return equipmentStartRunCheck; }
            set
            {
                if (value != equipmentStartRunCheck)
                {
                    equipmentStartRunCheck = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(SMT_EqpID.EquipmentStartRunCheck, value);
                }
            }
        }

        #endregion SVID

        #region DVID

        private static string lotID = "";

        public static string LotID
        {
            get { return lotID; }
            set
            {
                if (value != lotID)
                {
                    lotID = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(SMT_EqpID.LotID, value);
                }
            }
        }

        private static string magazineID = "";

        public static string MagazineID
        {
            get { return magazineID; }
            set
            {
                if (value != magazineID)
                {
                    magazineID = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(SMT_EqpID.MagazineID, value);
                }
            }
        }

        private static string pass1MagazineID = "";

        public static string Pass1MagazineID
        {
            get { return pass1MagazineID; }
            set
            {
                if (value != pass1MagazineID)
                {
                    pass1MagazineID = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(SMT_EqpID.Pass1MagazineID, value);
                }
            }
        }

        private static string pass2MagazineID = "";

        public static string Pass2MagazineID
        {
            get { return pass2MagazineID; }
            set
            {
                if (value != pass2MagazineID)
                {
                    pass2MagazineID = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(SMT_EqpID.Pass2MagazineID, value);
                }
            }
        }

        private static string nGMagazineID = "";

        public static string NGMagazineID
        {
            get { return nGMagazineID; }
            set
            {
                if (value != nGMagazineID)
                {
                    nGMagazineID = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(SMT_EqpID.NGMagazineID, value);
                }
            }
        }

        private static string emptyMagazineID = "";

        public static string EmptyMagazineID
        {
            get { return emptyMagazineID; }
            set
            {
                if (value != emptyMagazineID)
                {
                    emptyMagazineID = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(SMT_EqpID.EmptyMagazineID, value);
                }
            }
        }

        private static string boatBarcode = "";

        public static string BoatBarcode
        {
            get { return boatBarcode; }
            set
            {
                if (value != boatBarcode)
                {
                    boatBarcode = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(SMT_EqpID.BoatBarcode, value);
                }
            }
        }

        private static string passBoatBarcode = "";

        public static string PassBoatBarcode
        {
            get { return passBoatBarcode; }
            set
            {
                if (value != passBoatBarcode)
                {
                    passBoatBarcode = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(SMT_EqpID.PassBoatBarcode, value);
                }
            }
        }

        private static string nGBoatBarcode = "";

        public static string NGBoatBarcode
        {
            get { return nGBoatBarcode; }
            set
            {
                if (value != nGBoatBarcode)
                {
                    nGBoatBarcode = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(SMT_EqpID.NGBoatBarcode, value);
                }
            }
        }

        private static string emptyBoatBarcode = "";

        public static string EmptyBoatBarcode
        {
            get { return emptyBoatBarcode; }
            set
            {
                if (value != emptyBoatBarcode)
                {
                    emptyBoatBarcode = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(SMT_EqpID.EmptyBoatBarcode, value);
                }
            }
        }

        private static string boatAssingNGBoatBarcode = "";

        public static string BoatAssingNGBoatBarcode
        {
            get { return boatAssingNGBoatBarcode; }
            set
            {
                if (value != boatAssingNGBoatBarcode)
                {
                    boatAssingNGBoatBarcode = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(SMT_EqpID.BoatAssingNGBoatBarcode, value);
                }
            }
        }

        private static string recipeName = "";

        public static string RecipeName
        {
            get { return recipeName; }
            set
            {
                if (value != recipeName)
                {
                    recipeName = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(SMT_EqpID.RecipeName, value);
                }
            }
        }

        private static string pass1MagazineUnitCount = "";

        public static string Pass1MagazineUnitCount
        {
            get { return pass1MagazineUnitCount; }
            set
            {
                if (value != pass1MagazineUnitCount)
                {
                    pass1MagazineUnitCount = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(SMT_EqpID.Pass1MagazineUnitCount, value);
                }
            }
        }

        private static string pass2MagazineUnitCount = "";

        public static string Pass2MagazineUnitCount
        {
            get { return pass2MagazineUnitCount; }
            set
            {
                if (value != pass2MagazineUnitCount)
                {
                    pass2MagazineUnitCount = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(SMT_EqpID.Pass2MagazineUnitCount, value);
                }
            }
        }

        private static string nGMagazineUnitCount = "";

        public static string NGMagazineUnitCount
        {
            get { return nGMagazineUnitCount; }
            set
            {
                if (value != nGMagazineUnitCount)
                {
                    nGMagazineUnitCount = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(SMT_EqpID.NGMagazineUnitCount, value);
                }
            }
        }

        #endregion DVID
    }

    internal class FVI_EqpParameter
    {
        #region SVID

        private static string equipment_RemoteControlStatus = "";

        public static string Equipment_RemoteControlStatus
        {
            get { return equipment_RemoteControlStatus; }
            set
            {
                if (value != equipment_RemoteControlStatus)
                {
                    equipment_RemoteControlStatus = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(FVI_EqpID.Equipment_RemoteControlStatus, value);
                }
            }
        }

        private static string equipment_RemoteControlResult = "";

        public static string Equipment_RemoteControlResult
        {
            get { return equipment_RemoteControlResult; }
            set
            {
                if (value != equipment_RemoteControlResult)
                {
                    equipment_RemoteControlResult = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(FVI_EqpID.Equipment_RemoteControlResult, value);
                }
            }
        }

        private static string equipment_LocalControlStatus = "";

        public static string Equipment_LocalControlStatus
        {
            get { return equipment_LocalControlStatus; }
            set
            {
                if (value != equipment_LocalControlStatus)
                {
                    equipment_LocalControlStatus = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(FVI_EqpID.Equipment_LocalControlStatus, value);
                }
            }
        }

        private static string equipment_LocalControlResult = "";

        public static string Equipment_LocalControlResult
        {
            get { return equipment_LocalControlResult; }
            set
            {
                if (value != equipment_LocalControlResult)
                {
                    equipment_LocalControlResult = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(FVI_EqpID.Equipment_LocalControlResult, value);
                }
            }
        }

        private static string reel_Status = "";

        public static string Reel_Status
        {
            get { return reel_Status; }
            set
            {
                if (value != reel_Status)
                {
                    reel_Status = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(FVI_EqpID.Reel_Status, value);
                }
            }
        }

        private static string dieCount_CompareResult = "";

        public static string DieCount_CompareResult
        {
            get { return dieCount_CompareResult; }
            set
            {
                if (value != dieCount_CompareResult)
                {
                    dieCount_CompareResult = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(FVI_EqpID.DieCount_CompareResult, value);
                }
            }
        }

        #endregion SVID

        #region DVID

        private static string reelID = "";

        public static string ReelID
        {
            get { return reelID; }
            set
            {
                if (value != reelID)
                {
                    reelID = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(FVI_EqpID.ReelID, value);
                }
            }
        }

        private static string aPN = "";

        public static string APN
        {
            get { return aPN; }
            set
            {
                if (value != aPN)
                {
                    aPN = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(FVI_EqpID.APN, value);
                }
            }
        }

        private static string dateCode = "";

        public static string DateCode
        {
            get { return dateCode; }
            set
            {
                if (value != dateCode)
                {
                    dateCode = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(FVI_EqpID.DateCode, value);
                }
            }
        }

        private static string lotID = "";

        public static string LotID
        {
            get { return lotID; }
            set
            {
                if (value != lotID)
                {
                    lotID = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(FVI_EqpID.LotID, value);
                }
            }
        }

        private static string currentRecipeName = "";

        public static string CurrentRecipeName
        {
            get { return currentRecipeName; }
            set
            {
                if (value != currentRecipeName)
                {
                    currentRecipeName = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(FVI_EqpID.CurrentRecipeName, value);
                }
            }
        }

        private static string expected_DieCount = "";

        public static string Expected_DieCount
        {
            get { return expected_DieCount; }
            set
            {
                if (value != expected_DieCount)
                {
                    expected_DieCount = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(FVI_EqpID.Expected_DieCount, value);
                }
            }
        }

        private static string actual_DieCount = "";

        public static string Actual_DieCount
        {
            get { return actual_DieCount; }
            set
            {
                if (value != actual_DieCount)
                {
                    actual_DieCount = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(FVI_EqpID.Actual_DieCount, value);
                }
            }
        }

        #endregion DVID
    }

    public class TrimGap_EqpParameter
    {
        #region ECID

        private static string chunkRotateDistance = "";

        public static string ChunkRotateDistance
        {
            get { return chunkRotateDistance; }
            set
            {
                if (value != chunkRotateDistance)
                {
                    chunkRotateDistance = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateEC(TrimGap_EqpID.ChunkRotateDistance, value);
                }
            }
        }

        private static string cofficient = "";

        public static string Cofficient
        {
            get { return cofficient; }
            set
            {
                if (value != cofficient)
                {
                    cofficient = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateEC(TrimGap_EqpID.Cofficient, value);
                }
            }
        }

        private static string robotSpeed = "";

        public static string RobotSpeed
        {
            get { return robotSpeed; }
            set
            {
                if (value != robotSpeed)
                {
                    robotSpeed = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateEC(TrimGap_EqpID.RobotSpeed, value);
                }
            }
        }

        private static string motionRotate = "";

        public static string MotionRotate
        {
            get { return motionRotate; }
            set
            {
                if (value != motionRotate)
                {
                    motionRotate = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateEC(TrimGap_EqpID.MotionRotate, value);
                }
            }
        }

        private static string softWareVersion = "";

        public static string SoftWareVersion
        {
            get { return softWareVersion; }
            set
            {
                if (value != softWareVersion)
                {
                    softWareVersion = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateEC(TrimGap_EqpID.SoftWareVersion, value);
                }
            }
        }

        #endregion ECID

        #region DVID

        private static string equipmentStatus = "";

        public static string EquipmentStatus
        {
            get { return equipmentStatus; }
            set
            {
                if (value != equipmentStatus)
                {
                    equipmentStatus = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.EquipmentStatus, value);
                }
            }
        }

        private static string recipeID = "";

        public static string RecipeID
        {
            get { return recipeID; }
            set
            {
                if (value != recipeID)
                {
                    recipeID = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.RecipeID, value);
                }
            }
        }

        private static string loadport1_RecipeID = "";

        public static string Loadport1_RecipeID
        {
            get { return loadport1_RecipeID; }
            set
            {
                if (value != loadport1_RecipeID)
                {
                    loadport1_RecipeID = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.Loadport1_RecipeID, value);
                }
            }
        }

        private static string loadport2_RecipeID = "";

        public static string Loadport2_RecipeID
        {
            get { return loadport2_RecipeID; }
            set
            {
                if (value != loadport2_RecipeID)
                {
                    loadport2_RecipeID = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.Loadport2_RecipeID, value);
                }
            }
        }

        private static string carrierID = "";

        public static string CarrierID
        {
            get { return carrierID; }
            set
            {
                if (value != carrierID)
                {
                    carrierID = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.CarrierID, value);
                }
            }
        }

        private static int portID = 0;

        public static int PortID
        {
            get { return portID; }
            set
            {
                if (value != portID)
                {
                    portID = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.PortID, value);
                }
            }
        }

        private static int accessMode = 0;

        public static int AccessMode
        {
            get { return accessMode; }
            set
            {
                if (value != accessMode)
                {
                    accessMode = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.AccessMode, value);
                }
            }
        }

        private static int slotMapStatus = 0;

        public static int SlotMapStatus
        {
            get { return slotMapStatus; }
            set
            {
                if (value != slotMapStatus)
                {
                    slotMapStatus = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.SlotMapStatus, value);
                }
            }
        }

        private static int portTransferState = 0;

        public static int PortTransferState
        {
            get { return portTransferState; }
            set
            {
                if (value != portTransferState)
                {
                    portTransferState = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.PortTransferState, value);
                }
            }
        }

        private static int loadport1_AccessMode = 0;

        public static int Loadport1_AccessMode
        {
            get { return loadport1_AccessMode; }
            set
            {
                if (value != loadport1_AccessMode)
                {
                    loadport1_AccessMode = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.Loadport1_AccessMode, value);
                }
            }
        }

        private static int loadport2_AccessMode = 0;

        public static int Loadport2_AccessMode
        {
            get { return loadport2_AccessMode; }
            set
            {
                if (value != loadport2_AccessMode)
                {
                    loadport2_AccessMode = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.Loadport2_AccessMode, value);
                }
            }
        }

        private static int loadport1_PortTransferState = 0;

        public static int Loadport1_PortTransferState
        {
            get { return loadport1_PortTransferState; }
            set
            {
                if (value != loadport1_PortTransferState)
                {
                    loadport1_PortTransferState = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.Loadport1_PortTransferState, value);
                }
            }
        }

        private static int loadport2_PortTransferState = 0;

        public static int Loadport2_PortTransferState
        {
            get { return loadport2_PortTransferState; }
            set
            {
                if (value != loadport2_PortTransferState)
                {
                    loadport2_PortTransferState = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.Loadport2_PortTransferState, value);
                }
            }
        }

        private static int slotMap_List = 0;

        public static int SlotMap_List
        {
            get { return slotMap_List; }
            set
            {
                if (value != slotMap_List)
                {
                    slotMap_List = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.SlotMap_List, value);
                }
            }
        }

        private static int reason = 0;

        public static int Reason
        {
            get { return reason; }
            set
            {
                if (value != reason)
                {
                    reason = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.Reason, value);
                }
            }
        }

        private static int slotMap_1 = 0;

        public static int SlotMap_1
        {
            get { return slotMap_1; }
            set
            {
                if (value != slotMap_1)
                {
                    slotMap_1 = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.SlotMap_1, value);
                }
            }
        }

        private static int slotMap_2 = 0;

        public static int SlotMap_2
        {
            get { return slotMap_2; }
            set
            {
                if (value != slotMap_2)
                {
                    slotMap_2 = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.SlotMap_2, value);
                }
            }
        }

        private static int slotMap_3 = 0;

        public static int SlotMap_3
        {
            get { return slotMap_3; }
            set
            {
                if (value != slotMap_3)
                {
                    slotMap_3 = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.SlotMap_3, value);
                }
            }
        }

        private static int slotMap_4 = 0;

        public static int SlotMap_4
        {
            get { return slotMap_4; }
            set
            {
                if (value != slotMap_4)
                {
                    slotMap_4 = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.SlotMap_4, value);
                }
            }
        }

        private static int slotMap_5 = 0;

        public static int SlotMap_5
        {
            get { return slotMap_5; }
            set
            {
                if (value != slotMap_5)
                {
                    slotMap_5 = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.SlotMap_5, value);
                }
            }
        }

        private static int slotMap_6 = 0;

        public static int SlotMap_6
        {
            get { return slotMap_6; }
            set
            {
                if (value != slotMap_6)
                {
                    slotMap_6 = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.SlotMap_6, value);
                }
            }
        }

        private static int slotMap_7 = 0;

        public static int SlotMap_7
        {
            get { return slotMap_7; }
            set
            {
                if (value != slotMap_7)
                {
                    slotMap_7 = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.SlotMap_7, value);
                }
            }
        }

        private static int slotMap_8 = 0;

        public static int SlotMap_8
        {
            get { return slotMap_8; }
            set
            {
                if (value != slotMap_8)
                {
                    slotMap_8 = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.SlotMap_8, value);
                }
            }
        }

        private static int slotMap_9 = 0;

        public static int SlotMap_9
        {
            get { return slotMap_9; }
            set
            {
                if (value != slotMap_9)
                {
                    slotMap_9 = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.SlotMap_9, value);
                }
            }
        }

        private static int slotMap_10 = 0;

        public static int SlotMap_10
        {
            get { return slotMap_10; }
            set
            {
                if (value != slotMap_10)
                {
                    slotMap_10 = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.SlotMap_10, value);
                }
            }
        }

        private static int slotMap_11 = 0;

        public static int SlotMap_11
        {
            get { return slotMap_11; }
            set
            {
                if (value != slotMap_11)
                {
                    slotMap_11 = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.SlotMap_11, value);
                }
            }
        }

        private static int slotMap_12 = 0;

        public static int SlotMap_12
        {
            get { return slotMap_12; }
            set
            {
                if (value != slotMap_12)
                {
                    slotMap_12 = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.SlotMap_12, value);
                }
            }
        }

        private static int slotMap_13 = 0;

        public static int SlotMap_13
        {
            get { return slotMap_13; }
            set
            {
                if (value != slotMap_13)
                {
                    slotMap_13 = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.SlotMap_13, value);
                }
            }
        }

        private static int slotMap_14 = 0;

        public static int SlotMap_14
        {
            get { return slotMap_14; }
            set
            {
                if (value != slotMap_14)
                {
                    slotMap_14 = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.SlotMap_14, value);
                }
            }
        }

        private static int slotMap_15 = 0;

        public static int SlotMap_15
        {
            get { return slotMap_15; }
            set
            {
                if (value != slotMap_15)
                {
                    slotMap_15 = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.SlotMap_15, value);
                }
            }
        }

        private static int slotMap_16 = 0;

        public static int SlotMap_16
        {
            get { return slotMap_16; }
            set
            {
                if (value != slotMap_16)
                {
                    slotMap_16 = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.SlotMap_16, value);
                }
            }
        }

        private static int slotMap_17 = 0;

        public static int SlotMap_17
        {
            get { return slotMap_17; }
            set
            {
                if (value != slotMap_17)
                {
                    slotMap_17 = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.SlotMap_17, value);
                }
            }
        }

        private static int slotMap_18 = 0;

        public static int SlotMap_18
        {
            get { return slotMap_18; }
            set
            {
                if (value != slotMap_18)
                {
                    slotMap_18 = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.SlotMap_18, value);
                }
            }
        }

        private static int slotMap_19 = 0;

        public static int SlotMap_19
        {
            get { return slotMap_19; }
            set
            {
                if (value != slotMap_19)
                {
                    slotMap_19 = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.SlotMap_19, value);
                }
            }
        }

        private static int slotMap_20 = 0;

        public static int SlotMap_20
        {
            get { return slotMap_20; }
            set
            {
                if (value != slotMap_20)
                {
                    slotMap_20 = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.SlotMap_20, value);
                }
            }
        }

        private static int slotMap_21 = 0;

        public static int SlotMap_21
        {
            get { return slotMap_21; }
            set
            {
                if (value != slotMap_21)
                {
                    slotMap_21 = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.SlotMap_21, value);
                }
            }
        }

        private static int slotMap_22 = 0;

        public static int SlotMap_22
        {
            get { return slotMap_22; }
            set
            {
                if (value != slotMap_22)
                {
                    slotMap_22 = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.SlotMap_22, value);
                }
            }
        }

        private static int slotMap_23 = 0;

        public static int SlotMap_23
        {
            get { return slotMap_23; }
            set
            {
                if (value != slotMap_23)
                {
                    slotMap_23 = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.SlotMap_23, value);
                }
            }
        }

        private static int slotMap_24 = 0;

        public static int SlotMap_24
        {
            get { return slotMap_24; }
            set
            {
                if (value != slotMap_24)
                {
                    slotMap_24 = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.SlotMap_24, value);
                }
            }
        }

        private static int slotMap_25 = 0;

        public static int SlotMap_25
        {
            get { return slotMap_25; }
            set
            {
                if (value != slotMap_25)
                {
                    slotMap_25 = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.SlotMap_25, value);
                }
            }
        }

        private static string slot1_Info = "";

        public static string Slot1_Info
        {
            get { return slot1_Info; }
            set
            {
                if (value != slot1_Info)
                {
                    slot1_Info = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.Slot1_Info, value);
                }
            }
        }

        private static string slot2_Info = "";

        public static string Slot2_Info
        {
            get { return slot2_Info; }
            set
            {
                if (value != slot2_Info)
                {
                    slot2_Info = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.Slot2_Info, value);
                }
            }
        }

        private static string slot3_Info = "";

        public static string Slot3_Info
        {
            get { return slot3_Info; }
            set
            {
                if (value != slot3_Info)
                {
                    slot3_Info = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.Slot3_Info, value);
                }
            }
        }

        private static string slot4_Info = "";

        public static string Slot4_Info
        {
            get { return slot4_Info; }
            set
            {
                if (value != slot4_Info)
                {
                    slot4_Info = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.Slot4_Info, value);
                }
            }
        }

        private static string slot5_Info = "";

        public static string Slot5_Info
        {
            get { return slot5_Info; }
            set
            {
                if (value != slot5_Info)
                {
                    slot5_Info = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.Slot5_Info, value);
                }
            }
        }

        private static string slot6_Info = "";

        public static string Slot6_Info
        {
            get { return slot6_Info; }
            set
            {
                if (value != slot6_Info)
                {
                    slot6_Info = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.Slot6_Info, value);
                }
            }
        }

        private static string slot7_Info = "";

        public static string Slot7_Info
        {
            get { return slot7_Info; }
            set
            {
                if (value != slot7_Info)
                {
                    slot7_Info = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.Slot7_Info, value);
                }
            }
        }

        private static string slot8_Info = "";

        public static string Slot8_Info
        {
            get { return slot8_Info; }
            set
            {
                if (value != slot8_Info)
                {
                    slot8_Info = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.Slot8_Info, value);
                }
            }
        }

        private static string slot9_Info = "";

        public static string Slot9_Info
        {
            get { return slot9_Info; }
            set
            {
                if (value != slot9_Info)
                {
                    slot9_Info = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.Slot9_Info, value);
                }
            }
        }

        private static string slot10_Info = "";

        public static string Slot10_Info
        {
            get { return slot10_Info; }
            set
            {
                if (value != slot10_Info)
                {
                    slot10_Info = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.Slot10_Info, value);
                }
            }
        }

        private static string slot11_Info = "";

        public static string Slot11_Info
        {
            get { return slot11_Info; }
            set
            {
                if (value != slot11_Info)
                {
                    slot11_Info = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.Slot11_Info, value);
                }
            }
        }

        private static string slot12_Info = "";

        public static string Slot12_Info
        {
            get { return slot12_Info; }
            set
            {
                if (value != slot12_Info)
                {
                    slot12_Info = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.Slot12_Info, value);
                }
            }
        }

        private static string slot13_Info = "";

        public static string Slot13_Info
        {
            get { return slot13_Info; }
            set
            {
                if (value != slot13_Info)
                {
                    slot13_Info = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.Slot13_Info, value);
                }
            }
        }

        private static string slot14_Info = "";

        public static string Slot14_Info
        {
            get { return slot14_Info; }
            set
            {
                if (value != slot14_Info)
                {
                    slot14_Info = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.Slot14_Info, value);
                }
            }
        }

        private static string slot15_Info = "";

        public static string Slot15_Info
        {
            get { return slot15_Info; }
            set
            {
                if (value != slot15_Info)
                {
                    slot15_Info = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.Slot15_Info, value);
                }
            }
        }

        private static string slot16_Info = "";

        public static string Slot16_Info
        {
            get { return slot16_Info; }
            set
            {
                if (value != slot16_Info)
                {
                    slot16_Info = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.Slot16_Info, value);
                }
            }
        }

        private static string slot17_Info = "";

        public static string Slot17_Info
        {
            get { return slot17_Info; }
            set
            {
                if (value != slot17_Info)
                {
                    slot17_Info = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.Slot17_Info, value);
                }
            }
        }

        private static string slot18_Info = "";

        public static string Slot18_Info
        {
            get { return slot18_Info; }
            set
            {
                if (value != slot18_Info)
                {
                    slot18_Info = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.Slot18_Info, value);
                }
            }
        }

        private static string slot19_Info = "";

        public static string Slot19_Info
        {
            get { return slot19_Info; }
            set
            {
                if (value != slot19_Info)
                {
                    slot19_Info = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.Slot19_Info, value);
                }
            }
        }

        private static string slot20_Info = "";

        public static string Slot20_Info
        {
            get { return slot20_Info; }
            set
            {
                if (value != slot20_Info)
                {
                    slot20_Info = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.Slot20_Info, value);
                }
            }
        }

        private static string slot21_Info = "";

        public static string Slot21_Info
        {
            get { return slot21_Info; }
            set
            {
                if (value != slot21_Info)
                {
                    slot21_Info = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.Slot21_Info, value);
                }
            }
        }

        private static string slot22_Info = "";

        public static string Slot22_Info
        {
            get { return slot22_Info; }
            set
            {
                if (value != slot22_Info)
                {
                    slot22_Info = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.Slot22_Info, value);
                }
            }
        }

        private static string slot23_Info = "";

        public static string Slot23_Info
        {
            get { return slot23_Info; }
            set
            {
                if (value != slot23_Info)
                {
                    slot23_Info = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.Slot23_Info, value);
                }
            }
        }

        private static string slot24_Info = "";

        public static string Slot24_Info
        {
            get { return slot24_Info; }
            set
            {
                if (value != slot24_Info)
                {
                    slot24_Info = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.Slot24_Info, value);
                }
            }
        }

        private static string slot25_Info = "";

        public static string Slot25_Info
        {
            get { return slot25_Info; }
            set
            {
                if (value != slot25_Info)
                {
                    slot25_Info = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.Slot25_Info, value);
                }
            }
        }

        private static string slot1_Max = "";

        public static string Slot1_Max
        {
            get { return slot1_Max; }
            set
            {
                if (value != slot1_Max)
                {
                    slot1_Max = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.Slot1_Max, value);
                }
            }
        }

        private static string slot2_Max = "";

        public static string Slot2_Max
        {
            get { return slot2_Max; }
            set
            {
                if (value != slot2_Max)
                {
                    slot2_Max = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.Slot2_Max, value);
                }
            }
        }

        private static string slot3_Max = "";

        public static string Slot3_Max
        {
            get { return slot3_Max; }
            set
            {
                if (value != slot3_Max)
                {
                    slot3_Max = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.Slot3_Max, value);
                }
            }
        }

        private static string slot4_Max = "";

        public static string Slot4_Max
        {
            get { return slot4_Max; }
            set
            {
                if (value != slot4_Max)
                {
                    slot4_Max = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.Slot4_Max, value);
                }
            }
        }

        private static string slot5_Max = "";

        public static string Slot5_Max
        {
            get { return slot5_Max; }
            set
            {
                if (value != slot5_Max)
                {
                    slot5_Max = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.Slot5_Max, value);
                }
            }
        }

        private static string slot6_Max = "";

        public static string Slot6_Max
        {
            get { return slot6_Max; }
            set
            {
                if (value != slot6_Max)
                {
                    slot6_Max = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.Slot6_Max, value);
                }
            }
        }

        private static string slot7_Max = "";

        public static string Slot7_Max
        {
            get { return slot7_Max; }
            set
            {
                if (value != slot7_Max)
                {
                    slot7_Max = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.Slot7_Max, value);
                }
            }
        }

        private static string slot8_Max = "";

        public static string Slot8_Max
        {
            get { return slot8_Max; }
            set
            {
                if (value != slot8_Max)
                {
                    slot8_Max = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.Slot8_Max, value);
                }
            }
        }

        private static string slot9_Max = "";

        public static string Slot9_Max
        {
            get { return slot9_Max; }
            set
            {
                if (value != slot9_Max)
                {
                    slot9_Max = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.Slot9_Max, value);
                }
            }
        }

        private static string slot10_Max = "";

        public static string Slot10_Max
        {
            get { return slot10_Max; }
            set
            {
                if (value != slot10_Max)
                {
                    slot10_Max = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.Slot10_Max, value);
                }
            }
        }

        private static string slot11_Max = "";

        public static string Slot11_Max
        {
            get { return slot11_Max; }
            set
            {
                if (value != slot11_Max)
                {
                    slot11_Max = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.Slot11_Max, value);
                }
            }
        }

        private static string slot12_Max = "";

        public static string Slot12_Max
        {
            get { return slot12_Max; }
            set
            {
                if (value != slot12_Max)
                {
                    slot12_Max = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.Slot12_Max, value);
                }
            }
        }

        private static string slot13_Max = "";

        public static string Slot13_Max
        {
            get { return slot13_Max; }
            set
            {
                if (value != slot13_Max)
                {
                    slot13_Max = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.Slot13_Max, value);
                }
            }
        }

        private static string slot14_Max = "";

        public static string Slot14_Max
        {
            get { return slot14_Max; }
            set
            {
                if (value != slot14_Max)
                {
                    slot14_Max = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.Slot14_Max, value);
                }
            }
        }

        private static string slot15_Max = "";

        public static string Slot15_Max
        {
            get { return slot15_Max; }
            set
            {
                if (value != slot15_Max)
                {
                    slot15_Max = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.Slot15_Max, value);
                }
            }
        }

        private static string slot16_Max = "";

        public static string Slot16_Max
        {
            get { return slot16_Max; }
            set
            {
                if (value != slot16_Max)
                {
                    slot16_Max = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.Slot16_Max, value);
                }
            }
        }

        private static string slot17_Max = "";

        public static string Slot17_Max
        {
            get { return slot17_Max; }
            set
            {
                if (value != slot17_Max)
                {
                    slot17_Max = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.Slot17_Max, value);
                }
            }
        }

        private static string slot18_Max = "";

        public static string Slot18_Max
        {
            get { return slot18_Max; }
            set
            {
                if (value != slot18_Max)
                {
                    slot18_Max = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.Slot18_Max, value);
                }
            }
        }

        private static string slot19_Max = "";

        public static string Slot19_Max
        {
            get { return slot19_Max; }
            set
            {
                if (value != slot19_Max)
                {
                    slot19_Max = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.Slot19_Max, value);
                }
            }
        }

        private static string slot20_Max = "";

        public static string Slot20_Max
        {
            get { return slot20_Max; }
            set
            {
                if (value != slot20_Max)
                {
                    slot20_Max = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.Slot20_Max, value);
                }
            }
        }

        private static string slot21_Max = "";

        public static string Slot21_Max
        {
            get { return slot21_Max; }
            set
            {
                if (value != slot21_Max)
                {
                    slot21_Max = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.Slot21_Max, value);
                }
            }
        }

        private static string slot22_Max = "";

        public static string Slot22_Max
        {
            get { return slot22_Max; }
            set
            {
                if (value != slot22_Max)
                {
                    slot22_Max = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.Slot22_Max, value);
                }
            }
        }

        private static string slot23_Max = "";

        public static string Slot23_Max
        {
            get { return slot23_Max; }
            set
            {
                if (value != slot23_Max)
                {
                    slot23_Max = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.Slot23_Max, value);
                }
            }
        }

        private static string slot24_Max = "";

        public static string Slot24_Max
        {
            get { return slot24_Max; }
            set
            {
                if (value != slot24_Max)
                {
                    slot24_Max = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.Slot24_Max, value);
                }
            }
        }

        private static string slot25_Max = "";

        public static string Slot25_Max
        {
            get { return slot25_Max; }
            set
            {
                if (value != slot25_Max)
                {
                    slot25_Max = value;
                    SecsgemForm.g_lOperationResult = SecsgemForm.CGWrapper.UpdateSV(TrimGap_EqpID.Slot25_Max, value);
                }
            }
        }

        #endregion DVID
    }
}