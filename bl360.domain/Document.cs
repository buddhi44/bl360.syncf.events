using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bl360.domain
{
    public class User
    {
        public long UserKey { get; set; }
        public string UserName { get; set; } = "";
        public string Password { get; set; } = "";
        public string UserID { get; set; } = "";

        public override string ToString()
        {
            return UserID;
        }
    }

    public class Company
    {
        public int CompanyKey { get; set; }

        public string CompanyName { get; set; } = "";

        public string CompanyCode { get; set; } = "";
    }

    public class Document : BaseEntity
    {
        private int documentKey;
        private int companyKey;
        private string description = "";
        private string keyword = "";
        private string path = "";
        private string filename = "";
        private string versionNumber = "";
        private DateTime nextActionDate;
        private int projectKey;
        private CodeBaseResponse documentType;
        private CodeBaseResponse category;
        private long projectStatusKey;

        private int transactionKey;
        private string extention = "";
        private int employeeCodeKey;
        private int employeeCodeDtKey;

        private int buKey;
        private User uploadedBy;
        public int DocumentKey { get => documentKey; set => documentKey = value; }
        public int CompanyKey { get => companyKey; set => companyKey = value; }
        public string Description { get => description; set => description = value; }
        public string Keyword { get => keyword; set => keyword = value; }
        public string Path { get => path; set => path = value; }
        public string Filename { get => filename; set => filename = value; }
        public string VersionNumber { get => versionNumber; set => versionNumber = value; }
        public DateTime NextActionDate { get => nextActionDate; set => nextActionDate = value; }

        public CodeBaseResponse Category { get => category; set => category = value; }
        public long ProjectStatusKey { get => projectStatusKey; set => projectStatusKey = value; }
        public CodeBaseResponse DocumentType { get => documentType; set => documentType = value; }
        public int TransactionKey { get => transactionKey; set => transactionKey = value; }
        public int OrderKey { get; set; }
        public int ItemKey { get; set; }
        public int ProjectKey { get => projectKey; set => projectKey = value; }
        public int AddressKey { get; set; }
        public int ItemTranKey { get; set; }
        public int ProcessDetKey { get; set; }
        public int OrderDetailKey { get; set; }
        public string Extention { get => extention; set => extention = value; }
        public int CdKey { get; set; }

        public long FileSize { get; set; }



        public int EmployeeCodeKey { get => employeeCodeKey; set => employeeCodeKey = value; }
        public int EmployeeCodeDtKey { get => employeeCodeDtKey; set => employeeCodeDtKey = value; }
        public int BuKey { get => buKey; set => buKey = value; }
        public User UploadedBy { get => uploadedBy; set => uploadedBy = value; }
    }
    public class BinaryDocument : Document
    {
        public byte[] DocumentArray { get; set; }
    }

    public class Base64Document : Document
    {
        public string Base64Source { get; set; } = "";

        public string GetURL()
        {
            if (!string.IsNullOrEmpty(this.Base64Source))
            {
                return $"data:image/png;base64, {this.Base64Source}";
            }
            else
            {
                return "";
            }
        }

    }
}
