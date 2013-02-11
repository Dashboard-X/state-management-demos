using System;
using System.Reflection;

namespace Suprifattus.Util.Web
{
	public enum PersistLocation
	{
		Nowhere     = 0x00,
		Context     = 0x01,
		ViewState   = 0x02,
		Session     = 0x04,
		Application = 0x08,
	}

	[AttributeUsage(AttributeTargets.Field)]
	public class PersistFieldAttribute : Attribute
	{
		PersistLocation loc;
		string key;

		public PersistFieldAttribute()
			: this(PersistLocation.Nowhere, null)
		{
		}
		
		public PersistFieldAttribute(PersistLocation location)
			: this(location, null)
		{
		}

		public PersistFieldAttribute(PersistLocation location, string key)
		{
			Location = location;
			Key = key;
		}

		public string GetKeyFor(MemberInfo mi)
		{
			return (Key != null ? Key + "_" + mi.Name : mi.Name);
		}
		
		public string Key
		{
			get { return key; }
			set { key = value; }
		}

		public PersistLocation Location
		{
			get { return loc; }
			set { loc = value; }
		}

		public static PersistFieldAttribute GetAttribute(MemberInfo mi)
		{
			return (PersistFieldAttribute) Attribute.GetCustomAttribute(mi, typeof(PersistFieldAttribute));
		}

		public static PersistFieldAttribute GetAttribute(MemberInfo mi, PersistLocation forLocation)
		{
			PersistFieldAttribute attr = GetAttribute(mi);
			return (attr != null && attr.Location == forLocation ? attr : null);
		}
	}
}
