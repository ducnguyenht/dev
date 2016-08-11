using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace NAS.DAL.Nomenclature.Organization
{

    public sealed class DepartmentTypeConstant
    {
        private string value;

        public static readonly DepartmentTypeConstant NAAN_DEFAULT = new DepartmentTypeConstant("NAAN_DEFAULT");
        public static readonly DepartmentTypeConstant SUPPER_USER = new DepartmentTypeConstant("SUPPER_USER");
        public static readonly DepartmentTypeConstant ORPHAN = new DepartmentTypeConstant("ORPHAN");

        private DepartmentTypeConstant(string v)
        {
            value = v;
        }

        public override string ToString()
        {
            return value.ToString();
        }

        public string Value { get { return value; } }

    }

    public partial class DepartmentType
    {
        public DepartmentType(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
        #region
        public int CheckUniqueName()
        {
            return 1;
        }

        public int CheckConstraint()
        {
            return 1;
        }

        public static void Populate()
        {
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();
                //insert default data into DepartmentType table
                if (!Util.isExistXpoObject<DepartmentType>("Code", Utility.Constant.NAAN_DEFAULT_CODE))
                {
                    DepartmentType departmentType = new DepartmentType(session)
                    {
                        Code = Utility.Constant.NAAN_DEFAULT_CODE,
                        Name = Utility.Constant.NAAN_DEFAULT_NAME,
                        Description = "",
                        RowStatus = -1,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    departmentType.Save();
                }

                //insert SUPPER_USER department type
                if (!Util.isExistXpoObject<DepartmentType>("Code", "SUPPER_USER"))
                {
                    DepartmentType departmentType = new DepartmentType(session)
                    {
                        Code = "SUPPER_USER",
                        Name = "SUPPER_USER",
                        Description = "Quản trị viên tổ chức",
                        RowStatus = 1,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    departmentType.Save();
                }

                //insert ORPHAN department type
                if (!Util.isExistXpoObject<DepartmentType>("Code", "ORPHAN"))
                {
                    DepartmentType departmentType = new DepartmentType(session)
                    {
                        Code = "ORPHAN",
                        Name = "ORPHAN",
                        Description = "Chưa được phân bổ vào tổ chức",
                        RowStatus = 1,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    departmentType.Save();
                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (session != null) session.Dispose();
            }
        }
        #endregion
    }

}
