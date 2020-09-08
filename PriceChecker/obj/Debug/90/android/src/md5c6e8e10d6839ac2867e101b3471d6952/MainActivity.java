package md5c6e8e10d6839ac2867e101b3471d6952;


public class MainActivity
	extends android.support.v7.app.AppCompatActivity
	implements
		mono.android.IGCUserPeer,
		com.symbol.emdk.EMDKManager.EMDKListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"n_onResume:()V:GetOnResumeHandler\n" +
			"n_onPause:()V:GetOnPauseHandler\n" +
			"n_onDestroy:()V:GetOnDestroyHandler\n" +
			"n_onRequestPermissionsResult:(I[Ljava/lang/String;[I)V:GetOnRequestPermissionsResult_IarrayLjava_lang_String_arrayIHandler\n" +
			"n_onClosed:()V:GetOnClosedHandler:Symbol.XamarinEMDK.EMDKManager/IEMDKListenerInvoker, Symbol.XamarinEMDK\n" +
			"n_onOpened:(Lcom/symbol/emdk/EMDKManager;)V:GetOnOpened_Lcom_symbol_emdk_EMDKManager_Handler:Symbol.XamarinEMDK.EMDKManager/IEMDKListenerInvoker, Symbol.XamarinEMDK\n" +
			"";
		mono.android.Runtime.register ("PriceChecker.MainActivity, PriceChecker", MainActivity.class, __md_methods);
	}


	public MainActivity ()
	{
		super ();
		if (getClass () == MainActivity.class)
			mono.android.TypeManager.Activate ("PriceChecker.MainActivity, PriceChecker", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);


	public void onResume ()
	{
		n_onResume ();
	}

	private native void n_onResume ();


	public void onPause ()
	{
		n_onPause ();
	}

	private native void n_onPause ();


	public void onDestroy ()
	{
		n_onDestroy ();
	}

	private native void n_onDestroy ();


	public void onRequestPermissionsResult (int p0, java.lang.String[] p1, int[] p2)
	{
		n_onRequestPermissionsResult (p0, p1, p2);
	}

	private native void n_onRequestPermissionsResult (int p0, java.lang.String[] p1, int[] p2);


	public void onClosed ()
	{
		n_onClosed ();
	}

	private native void n_onClosed ();


	public void onOpened (com.symbol.emdk.EMDKManager p0)
	{
		n_onOpened (p0);
	}

	private native void n_onOpened (com.symbol.emdk.EMDKManager p0);

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
