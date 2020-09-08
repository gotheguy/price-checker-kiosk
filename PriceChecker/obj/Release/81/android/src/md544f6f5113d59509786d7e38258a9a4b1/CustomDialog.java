package md544f6f5113d59509786d7e38258a9a4b1;


public class CustomDialog
	extends android.app.Dialog
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("PriceChecker.Model.CustomDialog, PriceChecker", CustomDialog.class, __md_methods);
	}


	public CustomDialog (android.content.Context p0)
	{
		super (p0);
		if (getClass () == CustomDialog.class)
			mono.android.TypeManager.Activate ("PriceChecker.Model.CustomDialog, PriceChecker", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

	private java.util.ArrayList refList;
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
