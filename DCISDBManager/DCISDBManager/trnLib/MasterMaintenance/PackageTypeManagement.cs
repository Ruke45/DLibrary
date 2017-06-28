using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DCISDBManager.objLib.MasterMaintenance;
using DCISDBManager.trnLib.Utility;
using System.Configuration;
using DCISDBManager.trnLib.ParameterManagement;

namespace DCISDBManager.trnLib.MasterMaintenance
{
    public class PackageTypeManagement
    {
       
        public List<Packagetype> getPackageType(string PackageType)
        {
            try
            {

                List<Packagetype> lstpackage = new List<Packagetype>();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetPackageTypeResult> lst = datacontext.DCISgetPackageType(PackageType);

                    Packagetype pt;

                    foreach (DCISgetPackageTypeResult result in lst)
                    {
                        pt = new Packagetype();
                        pt.Package_Type = result.PackageType;
                        pt.Package_Description = result.PackageDescription;
                        pt.Is_Active = result.IsActive;
                        lstpackage.Add(pt);

                    }
                }

                return lstpackage;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }


        }

        public bool ModifyPackageType(DCISDBManager.objLib.MasterMaintenance.Packagetype Pt)
        {
            try
            {
                DCISLCDataContext datacontext = new DCISLCDataContext();

                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                datacontext.DCISModifyPackageTypes(Pt.Package_Type, Pt.Package_Description, Pt.Modified_By);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return false;
            }
        }

        public bool CreatePackageType(DCISDBManager.objLib.MasterMaintenance.Packagetype pt)
        {
            string PackageType= string.Empty;

            try
            {

                DCISLCDataContext datacontext = new DCISLCDataContext();
                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
               // PackageType
                SequenceManager seqmanager = new SequenceManager();
                Int64 ptype = seqmanager.getNextSequence("PackageType", datacontext);
                PackageType = "pgt" + ptype.ToString();

                datacontext.DCISsetPackageType(PackageType, pt.Package_Description, pt.Created_By, pt.Is_Active);


                return true;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return false;
            }

        }

        public List<Packagetype> getPackageTypen(string PackageTypen, string IsActiven)
        {
            try
            {

                List<Packagetype> lstGroup = new List<Packagetype>();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetPackageTypenResult> lst = datacontext.DCISgetPackageTypen(PackageTypen, IsActiven);

                    Packagetype grp;

                    foreach (DCISgetPackageTypenResult result in lst)
                    {
                        grp = new Packagetype();
                        grp.Package_Type = result.PackageType;
                        grp.Package_Description = result.PackageDescription;
                        grp.Is_Active = result.IsActive;
                        lstGroup.Add(grp);

                    }
                }

                return lstGroup;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }


        }

        public bool ModifyPackageTypeStatus(DCISDBManager.objLib.MasterMaintenance.Packagetype Pt)
        {
            try
            {
                DCISLCDataContext datacontext = new DCISLCDataContext();

                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                datacontext.DCISModifyPackageTypeStatus(Pt.Package_Type, Pt.Is_Active, Pt.Modified_By);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return false;
            }
        }

        public bool CheckPackageDescription(string PackageDescription)
        {
            try
            {
                DCISLCDataContext datacontext = new DCISLCDataContext();
                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                System.Data.Linq.ISingleResult<DCISCheckPackageDescriptionResult> Upd = datacontext.DCISCheckPackageDescription(PackageDescription);

                foreach (DCISCheckPackageDescriptionResult result in Upd)
                {
                    if (result.PackageDescription.Equals(PackageDescription))
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return false;
            }
        }


    }
}
