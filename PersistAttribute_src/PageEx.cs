using System;
using System.Reflection;
using System.Web.UI;

namespace Suprifattus.Util.Web
{
	public class PageEx : Page
	{
		const BindingFlags FieldBindingFlags = BindingFlags.Instance|BindingFlags.NonPublic;

		protected override void LoadViewState(object savedState)
		{
			base.LoadViewState(savedState);
			
			foreach (FieldInfo fi in GetType().GetFields(FieldBindingFlags))
			{
				PersistFieldAttribute attr = PersistFieldAttribute.GetAttribute(fi, PersistLocation.ViewState);
				if (attr != null)
				{
					TrySetValue(fi, ViewState[attr.GetKeyFor(fi)]);
				}
			}
		}

		protected override object SaveViewState()
		{
			foreach (FieldInfo fi in GetType().GetFields(FieldBindingFlags))
			{
				PersistFieldAttribute attr = PersistFieldAttribute.GetAttribute(fi, PersistLocation.ViewState);
				if (attr != null)
					ViewState[attr.GetKeyFor(fi)] = TryGetValue(fi);
			}
			return base.SaveViewState();
		}

		protected override void OnInit(EventArgs e)
		{
			foreach (FieldInfo fi in GetType().GetFields(FieldBindingFlags))
			{
				PersistFieldAttribute attr = PersistFieldAttribute.GetAttribute(fi);
				if (attr != null)
				{
					switch (attr.Location)
					{
						case PersistLocation.Application:
							TrySetValue(fi, Application[attr.GetKeyFor(fi)]);
							break;
						case PersistLocation.Context:
							TrySetValue(fi, Context.Items[attr.GetKeyFor(fi)]);
							break;
						case PersistLocation.Session:
							TrySetValue(fi, Session[attr.GetKeyFor(fi)]);
							break;
					}
				}
			}

			base.OnInit(e);
		}

		protected override void OnUnload(EventArgs e)
		{
			base.OnUnload(e);

			foreach (FieldInfo fi in GetType().GetFields(FieldBindingFlags))
			{
				PersistFieldAttribute attr = PersistFieldAttribute.GetAttribute(fi);
				if (attr != null)
				{
					switch (attr.Location)
					{
						case PersistLocation.Application:
							Application[attr.GetKeyFor(fi)] = TryGetValue(fi);
							break;
						case PersistLocation.Context:
							Context.Items[attr.GetKeyFor(fi)] = TryGetValue(fi);
							break;
						case PersistLocation.Session:
							Session[attr.GetKeyFor(fi)] = TryGetValue(fi);
							break;
					}
				}
			}
		}

		void TrySetValue(FieldInfo fi, object val)
		{
			if (val != null)
				fi.SetValue(this, val);
		}

		object TryGetValue(FieldInfo fi)
		{
			return fi.GetValue(this);
		}
	}
}
