package md5c6e8e10d6839ac2867e101b3471d6952;


public class BranchesListActivity
	extends android.support.v7.app.AppCompatActivity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("PriceChecker.BranchesListActivity, PriceChecker", BranchesListActivity.class, __md_methods);
	}


	public BranchesListActivity ()
	{
		super ();
		if (getClass () == BranchesListActivity.class)
			mono.android.TypeManager.Activate ("PriceChecker.BranchesListActivity, PriceChecker", "", this, new java.lang.Object[] {  });
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
