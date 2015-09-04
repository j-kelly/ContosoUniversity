namespace ContosoUniversity.Web.Core.Repository.Audit
{
    using System;
    using System.Reflection;

    public class AuditPropertyItem
    {
        public AuditPropertyItem(string propertyName, Func<object, object> transformation)
        {
            PropertyName = propertyName;
            Transformation = transformation;
        }

        public AuditPropertyItem(string propertyName, bool isRelationship = false)
        {
            PropertyName = propertyName;
            IsRelationship = isRelationship;
        }

        public string PropertyName
        {
            get;
            private set;
        }

        public PropertyInfo PropertyInfo
        {
            get;
            private set;
        }

        public Func<object, object> Transformation
        {
            get;
            private set;
        }

        public bool IsRelationship
        {
            get;
            private set;
        }

        public void SetPropertyInfo(Type classType)
        {
            PropertyInfo = classType.GetProperty(PropertyName);
        }

        public string GetValue(object obj)
        {
            if (Transformation != null)
            {
                var retVal = Transformation.Invoke(obj);
                return retVal.ToString();
            }

            return obj.ToString();
        }
    }
}
