﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.Interface;

namespace NAS.DAL.Staging.Accounting.Journal
{
    public partial class BusinessArtifactType : XPCustomObject, IDALValidate
    {
        public BusinessArtifactType(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        private Guid fBusinessArtifactTypeId;
        [Key(true)]
        public Guid BusinessArtifactTypeId
        {
            get { return fBusinessArtifactTypeId; }
            set { SetPropertyValue<Guid>("BusinessArtifactTypeId", ref fBusinessArtifactTypeId, value); }
        }

        private string fDescription;
        public string Description
        {
            get { return fDescription; }
            set { SetPropertyValue<string>("Description", ref fDescription, value); }
        }

        private string fName;
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue<string>("Name", ref fName, value); }
        }

        private byte fRowStatus;
        public byte RowStatus
        {
            get { return fRowStatus; }
            set { SetPropertyValue<byte>("RowStatus", ref fRowStatus, value); }
        }

        //link between 2 tables

        [Association(@"BusinessArtifactlReferencesBusinessArtifactType", typeof(BusinessArtifact))]
        public XPCollection<BusinessArtifact> BusinessArtifacts { get { return GetCollection<BusinessArtifact>("BusinessArtifacts"); } }
        
        //END link between 2 tables

        //validate database

        public bool ValidateParameter()
        {
            if (this.Name.Equals(string.Empty))
                return false;
            return true;
        }

        public bool ValidateUnique()
        {
            return true;
        }

        public bool IsExist()
        {
            return true;
            //throw new NotImplementedException();
        }

        protected override void OnSaving()
        {
            if (ValidateParameter())
            {
                if (ValidateUnique())
                    base.OnSaving();
            }
        }

        //END validate database
    }
}
