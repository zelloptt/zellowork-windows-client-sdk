#pragma once

#import "libid:{7E28D927-9F0F-4cc6-9BD9-E095B1574CCC}" raw_interfaces_only, raw_native_types, named_guids, auto_search, no_smart_pointers, no_auto_exclude, no_implementation

static ATL::_ATL_FUNC_INFO g_SinkBoolInfo = {CC_STDCALL, VT_EMPTY, 1, {VT_BOOL | VT_BYREF}};
static ATL::_ATL_FUNC_INFO g_SinkEnumInfo = {CC_STDCALL, VT_EMPTY, 1, {VT_I4}};
static ATL::_ATL_FUNC_INFO g_SinkDispatchInfo = {CC_STDCALL, VT_EMPTY, 1, {VT_DISPATCH}};
static ATL::_ATL_FUNC_INFO g_SinkDispatchBoolInfo = {CC_STDCALL, VT_EMPTY, 2, {VT_DISPATCH, VT_BOOL | VT_BYREF}};
static ATL::_ATL_FUNC_INFO g_SinkDispatchDispatchInfo = {CC_STDCALL, VT_EMPTY, 2, {VT_DISPATCH, VT_DISPATCH}};
static ATL::_ATL_FUNC_INFO g_SinkEmptyInfo = {CC_STDCALL, VT_EMPTY, 0, {}};

class CMainForm :
	public CDialogImpl<CMainForm>,
	public ATL::IDispEventSimpleImpl<1, CMainForm, &PttLib::DIID_IPttEvents>
{
	CAxWindow m_wndAx;
	CComPtr<PttLib::IPtt> m_spMesh;

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

public:
	enum { IDD = IDD_MAIN };

	CMainForm();

	BEGIN_MSG_MAP(CMainForm)
		MESSAGE_HANDLER(WM_INITDIALOG, OnInitDialog)
		MESSAGE_HANDLER(WM_DESTROY, OnDestroy)
		MESSAGE_HANDLER(WM_COMMAND, OnCommand)
		MESSAGE_HANDLER(WM_SIZE, OnSize)
		MESSAGE_HANDLER(WM_TIMER, OnTimer)
		NOTIFY_HANDLER(IDC_CONTACTS, LVN_ITEMACTIVATE, OnListViewItemActivate)
	END_MSG_MAP()

	BEGIN_SINK_MAP(CMainForm)
		SINK_ENTRY_INFO(1, PttLib::DIID_IPttEvents, PttLib::DISPID_ON_SIGN_IN_STARTED, &CMainForm::OnSignInStarted, &g_SinkEmptyInfo)
		SINK_ENTRY_INFO(1, PttLib::DIID_IPttEvents, PttLib::DISPID_ON_SIGN_IN_SUCCEEDED, &CMainForm::OnSignInSucceeded, &g_SinkEmptyInfo)
		SINK_ENTRY_INFO(1, PttLib::DIID_IPttEvents, PttLib::DISPID_ON_SIGN_IN_FAILED, &CMainForm::OnSignInFailed, &g_SinkEnumInfo)
		SINK_ENTRY_INFO(1, PttLib::DIID_IPttEvents, PttLib::DISPID_ON_SIGN_OUT_STARTED, &CMainForm::OnSignOutStarted, &g_SinkEmptyInfo)
		SINK_ENTRY_INFO(1, PttLib::DIID_IPttEvents, PttLib::DISPID_ON_SIGN_OUT_COMPLETE, &CMainForm::OnSignOutComplete, &g_SinkEmptyInfo)
		SINK_ENTRY_INFO(1, PttLib::DIID_IPttEvents, PttLib::DISPID_ON_CONTACT_LIST_CHANGED, &CMainForm::OnContactListChanged, &g_SinkEmptyInfo)
		SINK_ENTRY_INFO(1, PttLib::DIID_IPttEvents, PttLib::DISPID_ON_MESSAGE_IN_BEGIN, &CMainForm::OnMessageInBegin, &g_SinkDispatchInfo)
		SINK_ENTRY_INFO(1, PttLib::DIID_IPttEvents, PttLib::DISPID_ON_MESSAGE_IN_END, &CMainForm::OnMessageInEnd, &g_SinkDispatchInfo)
		SINK_ENTRY_INFO(1, PttLib::DIID_IPttEvents, PttLib::DISPID_ON_MESSAGE_OUT_BEGIN, &CMainForm::OnMessageOutBegin, &g_SinkDispatchDispatchInfo)
		SINK_ENTRY_INFO(1, PttLib::DIID_IPttEvents, PttLib::DISPID_ON_MESSAGE_OUT_END, &CMainForm::OnMessageOutEnd, &g_SinkDispatchDispatchInfo)
		SINK_ENTRY_INFO(1, PttLib::DIID_IPttEvents, PttLib::DISPID_ON_MESSAGE_OUT_ERROR, &CMainForm::OnMessageOutError, &g_SinkDispatchDispatchInfo)
		SINK_ENTRY_INFO(1, PttLib::DIID_IPttEvents, PttLib::DISPID_ON_AUDIO_MESSAGE_IN_START, &CMainForm::OnAudioMessageInStart, &g_SinkDispatchBoolInfo)
		SINK_ENTRY_INFO(1, PttLib::DIID_IPttEvents, PttLib::DISPID_ON_AUDIO_MESSAGE_IN_STOP, &CMainForm::OnAudioMessageInStop, &g_SinkDispatchInfo)
	END_SINK_MAP()

private:
	LRESULT OnInitDialog(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled);
	LRESULT OnDestroy(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled);
	LRESULT OnCommand(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled);
	LRESULT OnSize(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled);
	LRESULT OnTimer(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled);
	LRESULT OnListViewItemActivate(int idCtrl, LPNMHDR pnmh, BOOL& bHandled);

	void __stdcall OnSignInStarted();
	void __stdcall OnSignInSucceeded();
	void __stdcall OnSignInFailed(PttLib::CLIENT_ERROR);
	void __stdcall OnSignOutStarted();
	void __stdcall OnSignOutComplete();
	void __stdcall OnContactListChanged();
	void __stdcall OnMessageInBegin(IDispatch* pMessage);
	void __stdcall OnMessageInEnd(IDispatch* pMessage);
	void __stdcall OnMessageOutBegin(IDispatch* pMessage, IDispatch* pContact);
	void __stdcall OnMessageOutEnd(IDispatch* pMessage, IDispatch* pContact);
	void __stdcall OnMessageOutError(IDispatch* pMessage, IDispatch* pContact);
	void __stdcall OnAudioMessageInStart(IDispatch* pMessage, VARIANT_BOOL* pbVal);
	void __stdcall OnAudioMessageInStop(IDispatch* pMessage);

	void UpdateMode();
	void UpdateStatus();
	void UpdateContacts();
	void BeginMessage();
	void EndMessage();
};
