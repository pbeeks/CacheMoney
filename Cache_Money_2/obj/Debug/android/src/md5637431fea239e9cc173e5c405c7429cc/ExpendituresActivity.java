package md5637431fea239e9cc173e5c405c7429cc;


public class ExpendituresActivity
	extends android.app.ListActivity
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("Cache_Money_2.ExpendituresActivity, Cache_Money_2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", ExpendituresActivity.class, __md_methods);
	}


	public ExpendituresActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == ExpendituresActivity.class)
			mono.android.TypeManager.Activate ("Cache_Money_2.ExpendituresActivity, Cache_Money_2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

	java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
