#pragma once

#include "LoginDlg.h"

#import "libid:{7E28D927-9F0F-4cc6-9BD9-E095B1574CCC}" raw_interfaces_only, raw_native_types, named_guids, auto_search, no_smart_pointers, no_auto_exclude, no_implementation

static ATL::_ATL_FUNC_INFO g_SinkBoolInfo = {CC_STDCALL, VT_EMPTY, 1, {VT_BOOL | VT_BYREF}};
static ATL::_ATL_FUNC_INFO g_SinkEnumInfo = {CC_STDCALL, VT_EMPTY, 1, {VT_I4}};
static ATL::_ATL_FUNC_INFO g_SinkEmptyInfo = {CC_STDCALL, VT_EMPTY, 0, {}};


class CMainFrame :
	public ATL::CWindowImpl<CMainFrame, CWindow, ATL::CWinTraits<WS_OVERLAPPED | WS_SYSMENU | WS_CLIPCHILDREN, 0> >,
	public ATL::IDispEventSimpleImpl<1, CMainFrame, &PttLib::DIID_IPttEvents>,
	public CLoginDlgCb
{
	CAxWindow m_wndAx;
	ATL::CComPtr<PttLib::IPtt> m_spMesh;
	ATL::CComPtr<PttLib::ISettings> m_spSettings;
	ATL::CComPtr<IOleInPlaceActiveObject> m_spInPlace;
	bool m_bExiting;
	CLoginDlg m_dlgLogin;
	int m_iMinWidth, m_iMinHeight;

	void ManageControls();
	virtual void OnSignIn(const std::wstring& sUsername, const std::wstring& sPassword);

public:
	DECLARE_WND_CLASS_EX(0, CS_HREDRAW | CS_VREDRAW, 0)
	CMainFrame();
	~CMainFrame();

	bool PreTranslateMessage(MSG& msg);

	BEGIN_MSG_MAP(CMainFrame)
		MESSAGE_HANDLER(WM_CREATE, OnCreate)
		MESSAGE_HANDLER(WM_DESTROY, OnDestroy)
		MESSAGE_HANDLER(WM_SIZE, OnSize)
		MESSAGE_HANDLER(WM_CLOSE, OnClose)
		MESSAGE_HANDLER(WM_SETFOCUS, OnSetFocus)
		MESSAGE_HANDLER(WM_INITMENUPOPUP, OnInitMenuPopup)
		COMMAND_ID_HANDLER(ID_SIGNIN, OnCommand)
		COMMAND_ID_HANDLER(ID_SIGNOUT, OnCommand)
		COMMAND_ID_HANDLER(ID_HISTORY, OnCommand)
		COMMAND_ID_HANDLER(ID_CHANGEPASSWORD, OnCommand)
		COMMAND_ID_HANDLER(ID_OPTIONS, OnCommand)
		COMMAND_ID_HANDLER(ID_FEEDBACK, OnCommand)
		COMMAND_ID_HANDLER(ID_ABOUT, OnCommand)
		COMMAND_ID_HANDLER(ID_EXIT, OnCommand)
	END_MSG_MAP()

	BEGIN_SINK_MAP(CMainFrame)
		SINK_ENTRY_INFO(1, PttLib::DIID_IPttEvents, PttLib::DISPID_ON_SIGN_IN_STARTED, &CMainFrame::OnSignInStarted, &g_SinkEmptyInfo)
		SINK_ENTRY_INFO(1, PttLib::DIID_IPttEvents, PttLib::DISPID_ON_SIGN_IN_SUCCEEDED, &CMainFrame::OnSignInSucceeded, &g_SinkEmptyInfo)
		SINK_ENTRY_INFO(1, PttLib::DIID_IPttEvents, PttLib::DISPID_ON_SIGN_IN_FAILED, &CMainFrame::OnSignInFailed, &g_SinkEnumInfo)
		SINK_ENTRY_INFO(1, PttLib::DIID_IPttEvents, PttLib::DISPID_ON_SIGN_OUT_STARTED, &CMainFrame::OnSignOutStarted, &g_SinkEmptyInfo)
		SINK_ENTRY_INFO(1, PttLib::DIID_IPttEvents, PttLib::DISPID_ON_SIGN_OUT_COMPLETE, &CMainFrame::OnSignOutComplete, &g_SinkEmptyInfo)
		SINK_ENTRY_INFO(1, PttLib::DIID_IPttEvents, PttLib::DISPID_ON_SIGN_IN_REQUESTED, &CMainFrame::OnSignInRequested, &g_SinkEmptyInfo)
		SINK_ENTRY_INFO(1, PttLib::DIID_IPttEvents, PttLib::DISPID_ON_GET_CAN_SIGN_IN, &CMainFrame::OnGetCanSignIn, &g_SinkEmptyInfo)
	END_SINK_MAP()

	LRESULT OnCreate(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled);
	LRESULT OnDestroy(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled);
	LRESULT OnSize(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled);
	LRESULT OnClose(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled);
	LRESULT OnSetFocus(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled);
	LRESULT OnCommand(WORD wNotifyCode, WORD wID, HWND hWndCtl, BOOL& bHandled);
	LRESULT OnInitMenuPopup(UINT /*uMsg*/, WPARAM /*wParam*/, LPARAM /*lParam*/, BOOL& /*bHandled*/);

	void __stdcall OnSignInStarted();
	void __stdcall OnSignInSucceeded();
	void __stdcall OnSignInFailed(PttLib::CLIENT_ERROR);
	void __stdcall OnSignOutStarted();
	void __stdcall OnSignOutComplete();
	void __stdcall OnSignInRequested();
	void __stdcall OnGetCanSignIn(VARIANT_BOOL* pbVal);

};
