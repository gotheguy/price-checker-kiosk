package md5c6e8e10d6839ac2867e101b3471d6952;


public class DetailsActivity
	extends android.support.v7.app.AppCompatActivity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"n_onUserInteraction:()V:GetOnUserInteractionHandler\n" +
			"";
		mono.android.Runtime.register ("PriceChecker.DetailsActivity, PriceChecker", DetailsActivity.class, __md_methods);
	}


	public DetailsActivity ()
	{
		super ();
		if (getClass () == DetailsActivity.class)
			mono.android.TypeManager.Activate ("PriceChecker.DetailsActivity, PriceChecker", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);


	public void onUserInteraction ()
	{
		n_onUserInteraction ();
	}

	private native void n_onUserInteraction ();

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
