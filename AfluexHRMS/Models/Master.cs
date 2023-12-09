using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AfluexHRMS.Models
{
    public class Master : Common
    {
        #region Properties
        public List<Master> lstEarning { get; set; }
        public List<Master> lstDeduction { get; set; }
        public List<Master> lstLeave { get; set; }
        public List<Master> lstList { get; set; }
        public string CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        

        public string Pk_DepartmentId { get; set; }
        public List<Master> listDepartment { get; set; }
        public string DepartmentName { get; set; }
        public string DeletedBy { get; set; }

        public string Pk_DesignationID { get; set; }
        public List<Master> listDesignation { get; set; }
        public string Fk_DepartmentId { get; set; }
        public string DesignationName { get; set; }

        public string PK_SkillCategoryID { get; set; }
        public List<Master> listSkillCategory { get; set; }
        public string SkillCategoryCode { get; set; }
        public string SkillCategoryName { get; set; }
        public string RemarkSkillCategory { get; set; }

        public string PK_SalaryHeadID { get; set; }
        public List<Master> listSalaryHead { get; set; }
        public string SalaryHeadCode { get; set; }
        public string SalaryHeadName { get; set; }
        public string HeadNature { get; set; }
        public string AmountPersent { get; set; }
        public string RemarkSalaryHead { get; set; }

        public string Pk_LeaveTypeId { get; set; }
        public List<Master> listLeaveType { get; set; }
        public string LeaveType { get; set; }
        public string LeaveLimit { get; set; }
        public string Remark { get; set; }

        public string EmployeeID { get; set; }
        public string DesignationID { get; set; }
        public string DepartmentID { get; set; }
        public string FatherName { get; set; }
        public string Gender { get; set; }
        public string PhoneNo { get; set; }
        public string Address2 { get; set; }
        public string Pincode2 { get; set; }

        public string State2 { get; set; }
        public string City2 { get; set; }
        public string PFNo { get; set; }
        public string PAN { get; set; }
        public string BankAccountNo { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public string IFSCCOde { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string ProfilePic { get; set; }
        public string DateOfJoining { get; set; }
        public string DOB { get; set; }

        public string QualificationID { get; set; }
        public string Board { get; set; }
        public string DegreeCourse { get; set; }
        public string YearOfPassing { get; set; }
        public string Status { get; set; }
        public List<Master> lstQualification { get; set; }
        public List<SelectListItem> ddlDesignation { get; set; }

        public string WorkID { get; set; }
        public string PreviousCompany { get; set; }
        public string FromDate { get; set; }

        public string ToDate { get; set; }
        public string Post { get; set; }
        public string CertificateSubmitted { get; set; }
        public List<Master> lstWorkExperience { get; set; }

        public DataTable dtQualification { get; set; }
        public DataTable dtWorkExp { get; set; }

        public string LoginID { get; set; }
        public string Password { get; set; }
        public string VerifiedBy { get; set; }

        public List<Master> lstList1 { get; set; }
        public string SalaryHeadID { get; set; }
        public string IsAmtPer { get; set; }
        public string Value { get; set; }
        public string SalaryAmount { get; set; }

        public string LeaveID { get; set; }
        public string LeaveName { get; set; }

        public List<Master> lstListEmployee { get; set; }

        public string LeaveApplicationID { get; set; }
        public string UsedLeave { get; set; }

        public string HolidayID { get; set; }
        public string HolidayName { get; set; }
        public string HolidayDate { get; set; }


        public string SalaryDate { get; set; }
        public string PaymentDate { get; set; }
        public string Basic { get; set; }
        public string HRA { get; set; }
        public string Conveyance { get; set; }
        public string DA { get; set; }
        public string Bonus { get; set; }
        public string OtherMed { get; set; }
        public string Misce { get; set; }
        public string EPF { get; set; }
        public string ESICE { get; set; }
        public string PerDaySalary { get; set; }
        public string PF { get; set; }

        public string ESIC { get; set; }
        public string EFund { get; set; }
        public string Medical { get; set; }
        public string Loan { get; set; }
        public string Other { get; set; }
        public string TotalDeduction { get; set; }
        public string Noofdays { get; set; }
        public string TotalEarning { get; set; }

        public string NetSalary { get; set; }
        public string GIP { get; set; }
        public string OverTime { get; set; }
        public string OverTimeSalary { get; set; }

        public string PaidAmount { get; set; }
        public string Balance { get; set; }
        public string PaymentMode { get; set; }
        #endregion



        public DataSet GetStateCity1()
        {
            SqlParameter[] para = { new SqlParameter("@Pincode", Pincode2) };
            DataSet ds = DBHelper.ExecuteQuery("GetStateCity", para);
            return ds;
        }

        public DataSet LeaveApprovalReportBy()
        {
            SqlParameter[] para ={
                     new SqlParameter("@FK_EmpID",EmployeeID),
                     new SqlParameter("@EmployeeName",EmployeeName),
                     new SqlParameter("@EmployeeCode",EmployeeCode),
                            };
            DataSet ds = DBHelper.ExecuteQuery("LeaveApprovalReport", para);
            return ds;
        }

        #region Master
        public DataSet CompanyList()
        {
            DataSet ds = DBHelper.ExecuteQuery("CompanyList");
            return ds;
        }
        public DataSet AddCompany()
        {
            SqlParameter[] para =
                            {
                                 new SqlParameter("@CompanyName",CompanyName),
                                 new SqlParameter("@Address",Address),
                                 new SqlParameter("@PinCode",PinCode),
                                 new SqlParameter("@State",State),
                                 new SqlParameter("@City",City),
                                 new SqlParameter("@Contact",Contact),
                                 new SqlParameter("@Email",Email),
                                 new SqlParameter("@Website",Website),
                                 new SqlParameter("@AddedBy",AddedBy),

                            };
            DataSet ds = DBHelper.ExecuteQuery("AddCompany", para);
            return ds;
        }
        public DataSet UpdateCompany()
        {
            SqlParameter[] para =
                            {
                                new SqlParameter("@PK_CompanyID",CompanyID),
                                new SqlParameter("@CompanyName",CompanyName),
                                new SqlParameter("@Address",Address),
                                new SqlParameter("@PinCode",PinCode),
                                new SqlParameter("@State",State),
                                new SqlParameter("@City",City),
                                new SqlParameter("@Contact",Contact),
                                new SqlParameter("@Email",Email),
                                new SqlParameter("@Website",Website),
                                new SqlParameter("@UpdatedBy",AddedBy),

                            };
            DataSet ds = DBHelper.ExecuteQuery("UpdateCompany", para);
            return ds;
        }

        public DataSet DeleteCompany()
        {
            SqlParameter[] para =
                            {
                                new SqlParameter("@PK_CompanyID",CompanyID),
                                new SqlParameter("@DeletedBy",AddedBy),

                            };
            DataSet ds = DBHelper.ExecuteQuery("DeleteCompany", para);
            return ds;
        }
        #endregion

        #region Department
        public DataSet AddDepartment()
        {
            SqlParameter[] para =
                            {
                                new SqlParameter("@DepartmentName",DepartmentName),
                                new SqlParameter("@AddedBy",AddedBy),

                            };
            DataSet ds = DBHelper.ExecuteQuery("SaveDepartment", para);
            return ds;

        }

        public DataSet DepartmentDetails()
        {
            SqlParameter[] para =
                            {
                                new SqlParameter("@Pk_DepartmentId",Pk_DepartmentId),

                            };
            DataSet ds = DBHelper.ExecuteQuery("GetDepartmentList", para);

            return ds;

        }

        public DataSet UpdateDepartment()
        {
            SqlParameter[] para =
                            {
                                new SqlParameter("@DepartmentName",DepartmentName),
                                new SqlParameter("@Pk_DepartmentId",Pk_DepartmentId),
                                 new SqlParameter("@UpdatedBy",UpdatedBy)

                            };
            DataSet ds = DBHelper.ExecuteQuery("UpdateDepartment", para);
            return ds;

        }
        public DataSet DeleteDepartment()
        {
            SqlParameter[] para =
                            {
                                new SqlParameter("@Pk_DepartmentId",Pk_DepartmentId),
                                new SqlParameter("@DeletedBy",DeletedBy),

                            };
            DataSet ds = DBHelper.ExecuteQuery("DeleteDepartment", para);
            return ds;

        }
        #endregion

        #region Designation
        public DataSet InsertDesignation()
        {
            SqlParameter[] para =
                            {
                                new SqlParameter("@FK_DepartmentID",Fk_DepartmentId),
                                new SqlParameter("@DesignationName",DesignationName),
                                 new SqlParameter("@AddedBy",AddedBy)
                            };
            DataSet ds = DBHelper.ExecuteQuery("SaveDesignation", para);
            return ds;

        }
        public DataSet DesignationDetails()
        {
            SqlParameter[] para =
                            {
                                new SqlParameter("@Pk_DesignationID",Pk_DesignationID),
                            };
            DataSet ds = DBHelper.ExecuteQuery("GetDesignationList", para);
            return ds;

        }


        public DataSet UpdateDesignation()
        {
            SqlParameter[] para = {
                                new SqlParameter("@Pk_DesignationId",Pk_DesignationID),
                                new SqlParameter("@FK_DepartmentID",Fk_DepartmentId),
                                 new SqlParameter("@DesignationName",DesignationName),
                                 new SqlParameter("@UpdatedBy",UpdatedBy)
                            };
            DataSet ds = DBHelper.ExecuteQuery("UpdateDesignation", para);
            return ds;

        }

        public DataSet DeleteDesignation()
        {
            SqlParameter[] para = {
                                new SqlParameter("@Pk_DesignationId",Pk_DesignationID),
                                new SqlParameter("@DeletedBy",DeletedBy),
                            };
            DataSet ds = DBHelper.ExecuteQuery("DeleteDesignation", para);
            return ds;

        }
        #endregion

        #region Skill
        public DataSet SaveSkillCategory()
        {
            SqlParameter[] para = {
                    new SqlParameter("@SkillCategoryCode",SkillCategoryCode),
                    new SqlParameter("@SkillCategoryName",SkillCategoryName),
                    new SqlParameter("@RemarkSkillCategory",RemarkSkillCategory),
                    new SqlParameter("@AddedBy",AddedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("SaveSkillCategory", para);
            return ds;
        }

        public DataSet SkillCategoryList()
        {
            SqlParameter[] para = {
                    new SqlParameter("@Pk_SkillCategoryId",PK_SkillCategoryID),
            };
            DataSet ds = DBHelper.ExecuteQuery("GetSkillCategoryList", para);
            return ds;
        }

        public DataSet UpdateSkillCategory()
        {
            SqlParameter[] para = {

                    new SqlParameter("@Pk_SkillCategoryId",PK_SkillCategoryID),
                    new SqlParameter("@SkillCategoryCode",SkillCategoryCode),
                    new SqlParameter("@SkillCategoryName",SkillCategoryName),
                    new SqlParameter("@RemarkSkillCategory",RemarkSkillCategory),
                    new SqlParameter("@UpdatedBy",UpdatedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("UpdateSkillCategory", para);
            return ds;
        }

        public DataSet DeleteSkillCategory()
        {
            SqlParameter[] para = {

                    new SqlParameter("@Pk_SkillCategoryId",PK_SkillCategoryID),
                    new SqlParameter("@DeletedBy",DeletedBy),
            };
            DataSet ds = DBHelper.ExecuteQuery("DeleteSkillCategory", para);
            return ds;
        }
        #endregion

        #region Salary Head
        public DataSet SaveSalaryHead()
        {
            SqlParameter[] para = {
                    new SqlParameter("@SalaryHeadCode",SalaryHeadCode),
                    new SqlParameter("@SalaryHeadName",SalaryHeadName),
                    new SqlParameter("@HeadNature",HeadNature),
                    new SqlParameter("@IsAmtPer",AmountPersent),
                    new SqlParameter("@RemarkPerAmt",RemarkSalaryHead),
                    new SqlParameter("@AddedBy",AddedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("SaveSalaryHead", para);
            return ds;
        }

        public DataSet SalaryHeadList()
        {
            SqlParameter[] para = {

                    new SqlParameter("@Type",HeadNature),
                     new SqlParameter("@PK_SalaryHeadID",PK_SalaryHeadID),
            };
            DataSet ds = DBHelper.ExecuteQuery("SalaryHeadList", para);
            return ds;
        }


        public DataSet UpdateSalaryHead()
        {
            SqlParameter[] para = {

                    new SqlParameter("@PK_SalaryHeadID",PK_SalaryHeadID),
                    new SqlParameter("@SalaryHeadCode",SalaryHeadCode),
                    new SqlParameter("@SalaryHeadName",SalaryHeadName),
                    new SqlParameter("@HeadNature",HeadNature),
                    new SqlParameter("@IsAmtPer",AmountPersent),
                    new SqlParameter("@RemarkSalaryHead",RemarkSalaryHead),
                    new SqlParameter("@UpdatedBy",UpdatedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("UpdateSalaryHead", para);
            return ds;
        }


        public DataSet DeleteSalaryHead()
        {
            SqlParameter[] para = {

                    new SqlParameter("@Pk_SalaryHeadId",PK_SalaryHeadID),
                    new SqlParameter("@DeletedBy",DeletedBy),
            };
            DataSet ds = DBHelper.ExecuteQuery("DeleteSalaryHead", para);
            return ds;
        }
        #endregion

        #region Leave Type
        public DataSet SalveLeaveType()
        {
            SqlParameter[] para = {
                new SqlParameter("@LeaveType",LeaveType),
                new SqlParameter("@LeaveLimit",LeaveLimit),
                new SqlParameter("@RemarkLeave",Remark),
                new SqlParameter("@AddedBy",AddedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("SaveLeaveType", para);
            return ds;
        }

        public DataSet GetLeaveTypeDetails()
        {
            SqlParameter[] para = {
                new SqlParameter("@Pk_LeaveTypeId",Pk_LeaveTypeId)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetLeaveTypeList", para);
            return ds;
        }


        public DataSet UpdateLeaveType()
        {
            SqlParameter[] para = {
                    new SqlParameter("@PK_LeaveTypeId",Pk_LeaveTypeId),
                    new SqlParameter("@LeaveType",LeaveType),
                    new SqlParameter("@LeaveLimit",LeaveLimit),
                    new SqlParameter("@RemarkLeaveType",Remark),
                    new SqlParameter("@UpdatedBy",UpdatedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("UpdateLeaveType", para);
            return ds;
        }

        public DataSet DeleteLeaveType()
        {
            SqlParameter[] para = {
                    new SqlParameter("@Pk_LeavetypeId",Pk_LeaveTypeId),
                    new SqlParameter("@DeletedBy",DeletedBy),

            };
            DataSet ds = DBHelper.ExecuteQuery("DeleteLeaveType", para);
            return ds;
        }
        #endregion

        public DataSet DesignationList()
        {
            SqlParameter[] para = { new SqlParameter("@FK_DepartmentID",DepartmentID),};
            DataSet ds = DBHelper.ExecuteQuery("GetDesignationList", para);
            return ds;
        }

        #region Employee
        public DataSet DetailedEmployeeInfo()
        {
            SqlParameter[] para ={
                     new SqlParameter("@EmployeeID",EmployeeID),
                            };
            DataSet ds = DBHelper.ExecuteQuery("DetailedEmployeeInformation", para);
            return ds;
        }

        public DataSet DeleteQualification()
        {
            SqlParameter[] para = {
                     new SqlParameter("@Pk_Id",QualificationID),
                      new SqlParameter("@DeletedBy",AddedBy),
                            };
            DataSet ds = DBHelper.ExecuteQuery("DeleteQualification", para);
            return ds;
        }

        public DataSet DeleteWorkExperience()
        {
            SqlParameter[] para ={
                     new SqlParameter("@Pk_Id",WorkID),
                      new SqlParameter("@DeletedBy",AddedBy),
                            };
            DataSet ds = DBHelper.ExecuteQuery("DeleteWorkExp", para);
            return ds;
        }

        public DataSet SaveEmployeeBasicDetails()
        {
            SqlParameter[] para ={
                                  new SqlParameter("@FK_DepartmentID",DepartmentID),
                                  new SqlParameter("@FK_DesignationID",DesignationID),
                                  new SqlParameter("@EmployeeName",EmployeeName),
                                  new SqlParameter("@FatherName",FatherName),
                                  new SqlParameter("@DOB",DOB),
                                  new SqlParameter("@Gender",Gender),
                                  new SqlParameter("@MobileNo",Contact),
                                  new SqlParameter("@PhoneNo",PhoneNo),
                                  new SqlParameter("@PerAddress",Address),
                                  new SqlParameter("@PerPinCode",PinCode),
                                  new SqlParameter("@PerState",State),
                                  new SqlParameter("@PerCity",City),
                                  new SqlParameter("@Email",Email),
                                  new SqlParameter("@LocAddress",Address2),
                                  new SqlParameter("@LocPinCode",Pincode2),
                                  new SqlParameter("@LocState",State2),
                                  new SqlParameter("@LocCity",City2),
                                  new SqlParameter("@DateOfJoining",DateOfJoining),
                                  new SqlParameter("@PFNumber",PFNo),
                                  new SqlParameter("@PAN",PAN),
                                  new SqlParameter("@BankAccountNo",BankAccountNo),
                                  new SqlParameter("@BankName",BankName),
                                  new SqlParameter("@BankBranchName",BankBranch),
                                  new SqlParameter("@ProfilePic",ProfilePic),
                                  new SqlParameter("@dtEmployeeQualification",dtQualification),
                                  new SqlParameter("@dtWorkExperience",dtWorkExp),
                                  new SqlParameter("@AddedBy",AddedBy),
                                  new SqlParameter("@VerifiedBy",VerifiedBy),
                                  new SqlParameter("@VerRemark",Remark),
                                  new SqlParameter("@FK_CompanyID",CompanyID),
                                  new SqlParameter("@IFSCCOde",IFSCCOde),
                            };
            DataSet ds = DBHelper.ExecuteQuery("AddEmployeeBasicDetails", para);
            return ds;
        }

        public DataSet UpdateEmployeeDetails()
        {
            SqlParameter[] para ={
                                  new SqlParameter("@PK_EmployeeID",EmployeeID),
                                  new SqlParameter("@FK_DepartmentID",DepartmentID),
                                  new SqlParameter("@FK_DesignationID",DesignationID),
                                  new SqlParameter("@EmployeeName",EmployeeName),
                                  new SqlParameter("@FatherName",FatherName),
                                  new SqlParameter("@DOB",DOB),
                                  new SqlParameter("@Gender",Gender),
                                  new SqlParameter("@MobileNo",Contact),
                                  new SqlParameter("@PhoneNo",PhoneNo),
                                  new SqlParameter("@PerAddress",Address),
                                  new SqlParameter("@PerPinCode",PinCode),
                                  new SqlParameter("@PerState",State),
                                  new SqlParameter("@PerCity",City),
                                  new SqlParameter("@Email",Email),
                                  new SqlParameter("@LocAddress",Address2),
                                  new SqlParameter("@LocPinCode",Pincode2),
                                  new SqlParameter("@LocState",State2),
                                  new SqlParameter("@LocCity",City2),
                                  new SqlParameter("@DateOfJoining",DateOfJoining),
                                  new SqlParameter("@PFNumber",PFNo),
                                  new SqlParameter("@PAN",PAN),
                                  new SqlParameter("@BankAccountNo",BankAccountNo),
                                  new SqlParameter("@BankName",BankName),
                                  new SqlParameter("@BankBranchName",BankBranch),
                                  new SqlParameter("@ProfilePic",ProfilePic),
                                  new SqlParameter("@dtEmployeeQualification",dtQualification),
                                  new SqlParameter("@dtWorkExperience",dtWorkExp),
                                  new SqlParameter("@UpdatedBy",AddedBy),
                                  new SqlParameter("@FK_CompanyID",CompanyID),
                                  new SqlParameter("@IFSCCOde",IFSCCOde),
                            };
            DataSet ds = DBHelper.ExecuteQuery("UpdateEmployee", para);
            return ds;
        }

        public DataSet EmployeeReportBy()
        {
            SqlParameter[] para ={
             new SqlParameter("@EmployeeName", EmployeeName),
             new SqlParameter("@EmployeeCode", EmployeeCode),
             new SqlParameter("@CompanyID", CompanyID),
               new SqlParameter("@DepartmentID", DepartmentID),
             new SqlParameter("@DesignationID", DesignationID),
                            };
            DataSet ds = DBHelper.ExecuteQuery("EmployeeList", para);
            return ds;
        }

        public DataSet DeleteEmployee()
        {
            SqlParameter[] para = {
                     new SqlParameter("@EmployeeID",EmployeeID),
                     new SqlParameter("@DeletedBy",AddedBy),
                            };
            DataSet ds = DBHelper.ExecuteQuery("DeleteEmployee", para);
            return ds;
        }

        public DataSet DepartmentList()
        {
            DataSet ds = DBHelper.ExecuteQuery("GetDepartmentList");
            return ds;
        }
        #endregion

        #region Employee Salary
        public DataSet SaveEmployeeSalaryDetails()
        {
            SqlParameter[] para ={
                 new SqlParameter("@FK_EmpID",EmployeeID),
                     new SqlParameter("@dtEmployeeEarning",dtQualification),
                         new SqlParameter("@dtEmployeeDeduction",dtWorkExp),
                             new SqlParameter("@AddedBy",AddedBy),
                            };
            DataSet ds = DBHelper.ExecuteQuery("AddEmployeeSalary", para);
            return ds;
        }
        public DataSet EmployeeList()
        {
            SqlParameter[] para ={
                     new SqlParameter("@PK_EmployeeID",EmployeeID ),
                     new SqlParameter("@LoginID",LoginID),
                     new SqlParameter("@EmployeeCode",EmployeeCode),
                     new SqlParameter("@EmployeeName",EmployeeName),
                            };
            DataSet ds = DBHelper.ExecuteQuery("GetEmployeeDetails", para);
            return ds;
        }

        public DataSet EmployeeSalary()
        {
            SqlParameter[] para ={
                     new SqlParameter("@EmployeeID",EmployeeID),
                            };
            DataSet ds = DBHelper.ExecuteQuery("EmployeeSalaryDetails", para);
            return ds;
        }

        public DataSet UpdateSalary()
        {
            SqlParameter[] para = {
                 new SqlParameter("@FK_EmpID",EmployeeID),
                 new SqlParameter("@dtEmployeeEarning",dtQualification),
                 new SqlParameter("@dtEmployeeDeduction",dtWorkExp),
                 new SqlParameter("@UpdatedBy",AddedBy),
                            };
            DataSet ds = DBHelper.ExecuteQuery("UpdateEmployeeSalary", para);
            return ds;
        }
        #endregion

        #region Leave
        public DataSet LeaveTypeList()
        {
            SqlParameter[] para = {
                     new SqlParameter("@Pk_LeaveTypeId",LeaveID),
                            };
            DataSet ds = DBHelper.ExecuteQuery("GetLeaveType", para);
            return ds;
        }
        public DataSet AddEmployeeLeave()
        {
            SqlParameter[] para = {
                     new SqlParameter("@FK_EmpID",EmployeeID),
                     new SqlParameter("@AddedBy",AddedBy),
                            };
            DataSet ds = DBHelper.ExecuteQuery("SaveEmpoyeeLeave", para);
            return ds;
        }
        public DataSet GetFilterEmployeeLeave()
        {
            SqlParameter[] para = {
                     new SqlParameter("@EmployeeCode",EmployeeCode),
                     new SqlParameter("@EmployeeName",EmployeeName),
                            };
            DataSet ds = DBHelper.ExecuteQuery("FilterEmployeeLeave", para);
            return ds;
        }
        #endregion

        #region ListForLeaveApproval
        public DataSet ListForLeaveApprovalBy()
        {
            SqlParameter[] para ={
                     new SqlParameter("@FK_EmpID",EmployeeID),
                     new SqlParameter("@EmployeeName",EmployeeName),
                     new SqlParameter("@EmployeeCode",EmployeeCode), };
            DataSet ds = DBHelper.ExecuteQuery("GetLeaveApplication", para);
            return ds;
        }

        public DataSet ApproveLeave()
        {
            SqlParameter[] para ={
                     new SqlParameter("@PK_LeaveAppID",LeaveApplicationID),
                     new SqlParameter("@UsedLeave",UsedLeave),
                     new SqlParameter("@ApproverRemark",Remark),
                       new SqlParameter("@UpdatedBy",AddedBy),
                            };
            DataSet ds = DBHelper.ExecuteQuery("ApproveEmployeeLeave", para);
            return ds;
        }

        public DataSet RejectLeave()
        {
            SqlParameter[] para ={
                     new SqlParameter("@PK_LeaveAppID",LeaveApplicationID),
                     new SqlParameter("@ApproverRemark",Remark),
                     new SqlParameter("@UpdatedBy",AddedBy),
                            };
            DataSet ds = DBHelper.ExecuteQuery("DeclineEmployeeLeave", para);
            return ds;
        }


        #endregion

        #region HolidayMaster

        public DataSet HolidayList()
        {
            DataSet ds = DBHelper.ExecuteQuery("HolidayList");
            return ds;
        }

        public DataSet SaveHoliday()
        {
            SqlParameter[] para ={
                     new SqlParameter("@HolidayName",HolidayName),
                     new SqlParameter("@HolidayDate",HolidayDate),
                     new SqlParameter("@AddedBy",AddedBy),
                            };
            DataSet ds = DBHelper.ExecuteQuery("AddHoliday", para);
            return ds;
        }

        #endregion

        #region Payroll


        public DataSet SalaryPostingList()
        {
            SqlParameter[] para = {
             new SqlParameter("@FK_EmpId", EmployeeID),
             new SqlParameter("@SalaryDate", SalaryDate),
              new SqlParameter("@EmployeeCode", EmployeeCode),
             new SqlParameter("@EmployeeName", EmployeeName),
                            };
            DataSet ds = DBHelper.ExecuteQuery("GetSalaryPostingData", para);
            return ds;
        }

        public DataSet SaveEmployeeSalary()
        {
            SqlParameter[] para = {
             new SqlParameter("@SalaryDate", SalaryDate),
             new SqlParameter("@PaymentDate", PaymentDate),
             new SqlParameter("@dtSalary", dtQualification),
             new SqlParameter("@AddedBy", AddedBy),
                            };
            DataSet ds = DBHelper.ExecuteQuery("PostEmployeeSalary", para);
            return ds;
        }

        #endregion

        #region AdvanceSalaryPayment
        public DataSet SaveEmployeeAdvanceSalary()
        {
            SqlParameter[] para ={
             new SqlParameter("@PaymentDate", PaymentDate),
             new SqlParameter("@dtSalary", dtQualification),
             new SqlParameter("@AddedBy", AddedBy),
                            };
            DataSet ds = DBHelper.ExecuteQuery("AdvanceSalaryPayment", para);
            return ds;
        }
        #endregion

        #region SalaryPayment
        public DataSet PaymentModeList()
        {
            DataSet ds = DBHelper.ExecuteQuery("PaymentModeList");
            return ds;
        }

        public DataSet EmployeeListForSalaryPayment()
        {
            SqlParameter[] para = {
             new SqlParameter("@PaymentDate", SalaryDate),
             new SqlParameter("@EmployeeCode", EmployeeCode),
             new SqlParameter("@EmployeeName", EmployeeName),
                            };
            DataSet ds = DBHelper.ExecuteQuery("GetListForSalaryPayment", para);
            return ds;
        }

        public DataSet SaveEmployeeFinalSalary()
        {
            SqlParameter[] para ={
             new SqlParameter("@EmployeeSalary", dtQualification),
             new SqlParameter("@PaymentDate", PaymentDate),
             new SqlParameter("@AddedBy", AddedBy),
                            };
            DataSet ds = DBHelper.ExecuteQuery("EmployeeSalaryPayment", para);
            return ds;
        }

        #endregion

        public DataSet SalaryPaymentReportBy()
        {
            SqlParameter[] para ={
             new SqlParameter("@EmployeeName", EmployeeName),
             new SqlParameter("@EmployeeCode", EmployeeCode),
             new SqlParameter("@FromDate", FromDate),
               new SqlParameter("@ToDate", ToDate),
             new SqlParameter("@PaymentMode", PaymentMode),
                            };
            DataSet ds = DBHelper.ExecuteQuery("SalaryPaymentReport", para);
            return ds;
        }



        public string Fk_MainServiceTypeId { get; set; }
        public string Color { get; set; }
        public string EncCode { get; set; }
        public string Pk_ServiceId { get; set; }
        public string ServiceIcon { get; set; }
        public string Service { get; set; }
        public string ServiceUrl { get; set; }
        public string Category { get; set; }
        public string IsActive { get; set; }
        public string IsActiveDeactiveDate { get; set; }
        public List<Master> lst { get; set; }


        public DataSet GetService()
        {
            SqlParameter[] para ={
                                   new SqlParameter("@Fk_MainServiceTypeId",Fk_MainServiceTypeId),
                                   new SqlParameter("@FK_UserId",Fk_UserId),};
            DataSet ds = DBHelper.ExecuteQuery("GetService", para);
            return ds;
        }

    }
}