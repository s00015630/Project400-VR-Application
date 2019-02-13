using UnityEngine;
using System.Collections;

public class BrowserOpener : MonoBehaviour {

	public string pageToOpen = "https://mailitsligo-my.sharepoint.com/:p:/r/personal/s00015630_mail_itsligo_ie/_layouts/15/Doc.aspx?sourcedoc=%7B721A0619-5FC1-449C-A00B-40C6E22C4C19%7D&file=Presentation%201.pptx&action=edit&mobileredirect=true&wdNewAndOpenCt=1549644861148&wdPreviousSession=27b68eb5-0b58-4bb7-82e4-d6bccf353a48&wdOrigin=ohpAppStartPages";

	// check readme file to find out how to change title, colors etc.
	public void OnButtonClicked() {
		InAppBrowser.DisplayOptions options = new InAppBrowser.DisplayOptions();
		options.displayURLAsPageTitle = false;
		options.pageTitle = "Powerpoint Slides";

		InAppBrowser.OpenURL(pageToOpen, options);
	}

	public void OnClearCacheClicked() {
		InAppBrowser.ClearCache();
	}
}
