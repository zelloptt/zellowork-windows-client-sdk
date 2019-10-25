#pragma once
// Import Zello COM Control
#import "libid:{5069366D-44C6-4D86-946A-8CDF8E5552EC}" raw_interfaces_only, raw_native_types, named_guids, auto_search, no_auto_exclude, no_implementation

class CMainForm :
	public CDialogImpl<CMainForm>,
	public ATL::IDispEventImpl<1, CMainForm, &ZelloPTTLib::DIID_IZelloClientEvents, &ZelloPTTLib::LIBID_ZelloPTTLib, 1, 0>
{
	CAxWindow m_wndAx;
	CComPtr<ZelloPTTLib::IZelloClient> m_spMesh;
	WORD			m_wExitCode;
	CWindow			m_stUsername;
	CWindow			m_cbUsername;
	CWindow			m_stPassword;
	CWindow			m_edPassword;
	CWindow			m_lvContacts;
	CWindow			m_btButton;
	CWindow			m_stStatus;

	std::map<unsigned, std::wstring> m_Ids;

	enum Mode { mLogin, mContacts }
					m_Mode;
	enum State { sUnpushed, sPushed }
					m_State;
	ZelloPTTLib::NETWORK_STATUS m_NetworkStatus;

public:
	enum { IDD = IDD_MAIN };

	CMainForm();

	BEGIN_MSG_MAP(CMainForm)
		MESSAGE_HANDLER(WM_INITDIALOG, OnInitDialog)
		MESSAGE_HANDLER(WM_DESTROY, OnDestroy)
		MESSAGE_HANDLER(WM_COMMAND, OnCommand)
		MESSAGE_HANDLER(WM_SIZE, OnSize)
		MESSAGE_HANDLER(WM_TIMER, OnTimer)
		MESSAGE_HANDLER(WM_USER, OnExitAllowed)
		NOTIFY_HANDLER(IDC_CONTACTS, LVN_ITEMACTIVATE, OnListViewItemActivate)
	END_MSG_MAP()

	BEGIN_SINK_MAP(CMainForm)
        SINK_ENTRY_EX(1, ZelloPTTLib::DIID_IZelloClientEvents, ZelloPTTLib::DISPID_ON_EVENT, &CMainForm::OnEvent)
        SINK_ENTRY_EX(1, ZelloPTTLib::DIID_IZelloClientEvents, ZelloPTTLib::DISPID_ON_SIGN_IN_STARTED, &CMainForm::OnSignInStarted)
        SINK_ENTRY_EX(1, ZelloPTTLib::DIID_IZelloClientEvents, ZelloPTTLib::DISPID_ON_SIGN_IN_SUCCEEDED, &CMainForm::OnSignInSucceeded)
        SINK_ENTRY_EX(1, ZelloPTTLib::DIID_IZelloClientEvents, ZelloPTTLib::DISPID_ON_SIGN_IN_FAILED, &CMainForm::OnSignInFailed)
        SINK_ENTRY_EX(1, ZelloPTTLib::DIID_IZelloClientEvents, ZelloPTTLib::DISPID_ON_SIGN_OUT_STARTED, &CMainForm::OnSignOutStarted)
        SINK_ENTRY_EX(1, ZelloPTTLib::DIID_IZelloClientEvents, ZelloPTTLib::DISPID_ON_SIGN_OUT_COMPLETE, &CMainForm::OnSignOutComplete)
        SINK_ENTRY_EX(1, ZelloPTTLib::DIID_IZelloClientEvents, ZelloPTTLib::DISPID_ON_CONTACT_LIST_CHANGED, &CMainForm::OnContactListChanged)
        SINK_ENTRY_EX(1, ZelloPTTLib::DIID_IZelloClientEvents, ZelloPTTLib::DISPID_ON_MESSAGE_IN_BEGIN, &CMainForm::OnMessageInBegin)
        SINK_ENTRY_EX(1, ZelloPTTLib::DIID_IZelloClientEvents, ZelloPTTLib::DISPID_ON_MESSAGE_IN_END, &CMainForm::OnMessageInEnd)
        SINK_ENTRY_EX(1, ZelloPTTLib::DIID_IZelloClientEvents, ZelloPTTLib::DISPID_ON_MESSAGE_OUT_BEGIN, &CMainForm::OnMessageOutBegin)
        SINK_ENTRY_EX(1, ZelloPTTLib::DIID_IZelloClientEvents, ZelloPTTLib::DISPID_ON_MESSAGE_OUT_END, &CMainForm::OnMessageOutEnd)
        SINK_ENTRY_EX(1, ZelloPTTLib::DIID_IZelloClientEvents, ZelloPTTLib::DISPID_ON_MESSAGE_OUT_ERROR, &CMainForm::OnMessageOutError)
        SINK_ENTRY_EX(1, ZelloPTTLib::DIID_IZelloClientEvents, ZelloPTTLib::DISPID_ON_AUDIO_MESSAGE_IN_START, &CMainForm::OnAudioMessageInStart)
        SINK_ENTRY_EX(1, ZelloPTTLib::DIID_IZelloClientEvents, ZelloPTTLib::DISPID_ON_AUDIO_MESSAGE_IN_STOP, &CMainForm::OnAudioMessageInStop)
	END_SINK_MAP()

private:
	LRESULT OnInitDialog(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled);
	LRESULT OnDestroy(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled);
	LRESULT OnCommand(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled);
	LRESULT OnSize(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled);
	LRESULT OnTimer(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled);
	LRESULT OnExitAllowed(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled);
	LRESULT OnListViewItemActivate(int idCtrl, LPNMHDR pnmh, BOOL& bHandled);
    
	void __stdcall OnEvent(ZelloPTTLib::IZelloEvent* pEvent);
	void __stdcall OnSignInStarted();
	void __stdcall OnSignInSucceeded();
	void __stdcall OnSignInFailed(ZelloPTTLib::CLIENT_ERROR);
	void __stdcall OnSignOutStarted();
	void __stdcall OnSignOutComplete();
	void __stdcall OnContactListChanged();
	void __stdcall OnMessageInBegin(ZelloPTTLib::IMessage* pMessage);
	void __stdcall OnMessageInEnd(ZelloPTTLib::IMessage* pMessage);
	void __stdcall OnMessageOutBegin(ZelloPTTLib::IMessage* pMessage, ZelloPTTLib::IContact* pContact);
	void __stdcall OnMessageOutEnd(ZelloPTTLib::IMessage* pMessage, ZelloPTTLib::IContact* pContact);
	void __stdcall OnMessageOutError(ZelloPTTLib::IMessage* pMessage, ZelloPTTLib::IContact* pContact);
	void __stdcall OnAudioMessageInStart(ZelloPTTLib::IAudioInMessage* pMessage, VARIANT_BOOL* pbVal);
	void __stdcall OnAudioMessageInStop(ZelloPTTLib::IAudioInMessage* pMessage);

	void UpdateMode();
	void UpdateStatus();
	void UpdateContacts();
	void BeginMessage();
	void EndMessage();
	void SendText();
	void SendLocation();
	bool BeginSigningOut();
	void GetSelectedContactIds(std::queue<std::wstring>* pQ);
};
