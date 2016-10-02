using System;
using DevExpress.Xpo;
using DevExpress.Data;
using DevExpress.Xpo.DB;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Scheduler;

namespace WinWebSolution.Module {
    public class SchedulerActivityListViewController : ViewController<ListView> {
        private object masterDetailViewEmployeeIdCore;
        private SchedulerListEditorBase schedulerListEditorCore;
        public SchedulerActivityListViewController() {
            TargetObjectType = typeof(Activity);
        }
        protected override void OnActivated() {
            base.OnActivated();
            if (!IsAdmininstator(SecuritySystem.CurrentUserId)) {
                if (View.IsRoot) {
                    FilterActivities(CriteriaOperator.Parse("Employees[Oid = ?]", SecuritySystem.CurrentUserId));
                }
            }
            if (!View.IsRoot) {
                PropertyCollectionSource propertyCollectionSource = View.CollectionSource as PropertyCollectionSource;
                if (propertyCollectionSource != null) {
                    propertyCollectionSource.MasterObjectChanged += propertyCollectionSource_MasterObjectChanged;
                    UpdateMasterDetailViewEmployee(propertyCollectionSource);
                }
            }
            schedulerListEditorCore = ((ListView)View).Editor as SchedulerListEditorBase;
            if (schedulerListEditorCore != null) {
                schedulerListEditorCore.ResourceDataSourceCreated += schedulerListEditorCore_ResourceDataSourceCreated;
            }
        }
        protected override void OnDeactivated() {
            if (schedulerListEditorCore != null) {
                schedulerListEditorCore.ResourceDataSourceCreated -= schedulerListEditorCore_ResourceDataSourceCreated;
            }
            if (!View.IsRoot) {
                PropertyCollectionSource propertyCollectionSource = View.CollectionSource as PropertyCollectionSource;
                if (propertyCollectionSource != null) {
                    propertyCollectionSource.MasterObjectChanged -= propertyCollectionSource_MasterObjectChanged;
                }
            }
            base.OnDeactivated();
        }
        private void propertyCollectionSource_MasterObjectChanged(object sender, EventArgs e) {
            UpdateMasterDetailViewEmployee((PropertyCollectionSource)sender);
        }
        private void UpdateMasterDetailViewEmployee(PropertyCollectionSource propertyCollectionSource) {
            if (propertyCollectionSource == null) return;
            Employee masterDetailViewEmployee = propertyCollectionSource.MasterObject as Employee;
            if (masterDetailViewEmployee != null) {
                masterDetailViewEmployeeIdCore = masterDetailViewEmployee.Oid;
                FilterActivities(GetActivitiesFilter(masterDetailViewEmployee));
            }
        }
        private void schedulerListEditorCore_ResourceDataSourceCreated(object sender, ResourceDataSourceCreatedEventArgs e) {
            SortResources(GetResources(e.DataSource));
            FilterResources(GetResources(e.DataSource), GetResourcesFilter(GetEmployeeById(masterDetailViewEmployeeIdCore)));
        }
        private CriteriaOperator GetActivitiesFilter(Employee masterDetailViewEmployee) {
            CriteriaOperator criteria = null;
            if (masterDetailViewEmployee != null) {
                if (!IsAdmininstator(SecuritySystem.CurrentUserId)) {
                    if ((View.ObjectSpace.IsNewObject(masterDetailViewEmployee) && IsAdmininstator(masterDetailViewEmployee))
                            || (!View.ObjectSpace.IsNewObject(masterDetailViewEmployee) && IsAdmininstator(masterDetailViewEmployee))) {
                        criteria = CollectionSource.EmptyCollectionCriteria;
                    }
                }
                else {
                    criteria = CriteriaOperator.Parse("Employees[Oid = ?]", masterDetailViewEmployee.Oid);
                }
            }
            return criteria;
        }
        private CriteriaOperator GetResourcesFilter(Employee masterDetailViewEmployee) {
            CriteriaOperator criteria = null;
            bool isCurrentUserAdmin = IsAdmininstator(SecuritySystem.CurrentUserId);
            bool isViewRoot = View.IsRoot;
            bool isMasterDetailEmployeeNotEmpty = masterDetailViewEmployee != null;
            if (!isCurrentUserAdmin) {
                if (!isViewRoot && isMasterDetailEmployeeNotEmpty) {
                    if (IsAdmininstator(masterDetailViewEmployee)) {
                        criteria = CollectionSource.EmptyCollectionCriteria;
                    }
                    else {
                        criteria = CriteriaOperator.Parse("Oid = ?", masterDetailViewEmployee.Oid);
                    }
                }
                else if (isViewRoot) {
                    criteria = CriteriaOperator.Parse("Oid = ?", SecuritySystem.CurrentUserId);
                }
            }
            if (isCurrentUserAdmin && !isViewRoot && isMasterDetailEmployeeNotEmpty) {
                criteria = CriteriaOperator.Parse("Oid = ?", masterDetailViewEmployee.Oid);
            }
            return criteria;
        }
        private Employee GetEmployeeById(object employeeId) {
            return Application.CreateObjectSpace().GetObjectByKey<Employee>(employeeId);
        }
        private bool IsAdmininstator(object employeeId) {
            return IsAdmininstator(GetEmployeeById(employeeId));
        }
        private static bool IsAdmininstator(Employee employee) {
            Guard.ArgumentNotNull(employee, "employee");
            return Convert.ToBoolean(employee.Evaluate(CriteriaOperator.Parse("Groups[Name = ?].Count() > 0", Group.DefaultAdministratorsGroupName)));
        }
        private void FilterActivities(CriteriaOperator criteria) {
            View.CollectionSource.Criteria["FilterActivitiesByEmployee"] = criteria;
        }
        protected void SortResources(XPCollection resources) {
            if (resources != null) {
                IModelListView resourcesListView = Application.FindModelClass(resources.ObjectType).DefaultListView;
                XpoSortingHelper.Sort(resources, XpoSortingHelper.GetListViewSorting(resourcesListView));
            }
        }
        protected virtual void FilterResources(XPCollection resources, CriteriaOperator criteria) {
            if (resources != null && !ReferenceEquals(criteria, null)) {
                resources.Criteria = criteria;
            }
        }
        protected virtual XPCollection GetResources(object resourcesDataSource) {
            return resourcesDataSource as XPCollection;
        }
    }
    public class XpoSortingHelper {
        public static SortingCollection GetListViewSorting(IModelListView modelListView) {
            List<SortProperty> sorting = new List<SortProperty>(modelListView.Columns.Count);
            foreach (IModelColumn column in modelListView.Columns) {
                if (column.SortOrder != ColumnSortOrder.None && column.SortIndex >= 0) {
                    SortingDirection direction = SortingDirection.Ascending;
                    if (column.SortOrder == ColumnSortOrder.Descending)
                        direction = SortingDirection.Descending;
                    sorting.Insert(column.SortIndex, new SortProperty(column.PropertyName, direction));
                }
            }
            return new SortingCollection(sorting.ToArray());
        }
        public static void Sort(XPBaseCollection collection, SortingCollection sorting) {
            collection.Sorting = sorting; ;
        }
        public static void Sort(XPBaseCollection collection, string property, SortingDirection direction) {
            bool isSortingAdded = false;
            foreach (SortProperty sortProperty in collection.Sorting) {
                if (sortProperty.Property.Equals(CriteriaOperator.Parse(property))) {
                    isSortingAdded = true;
                }
            }
            if (!isSortingAdded) {
                collection.Sorting.Add(new SortProperty(property, direction));
            }
        }
    }
}