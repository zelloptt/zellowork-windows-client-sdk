#pragma once

class CLoginDlgCb
{
public:
	virtual ~CLoginDlgCb() {}
	virtual void OnSignIn(const std::wstring& sUsername, const std::wstring& sPassword) = 0;
};

class CLoginDlg :
	public ATL::CDialogImpl<CLoginDlg>
{
	CLoginDlgCb* m_pCb;
	ATL::CWindow m_wndFocused;

public:
	CLoginDlg(CLoginDlgCb* pCb);
	~CLoginDlg();

	void ShowFocus();
	void EnableControls(bool bEnable);

	std::wstring GetUsername() const;
	std::wstring GetPassword() const;
	void SetUsername(const std::wstring& sUsername);
	void AddUsername(const std::wstring& sUsername);
	void SetPassword(const std::wstring& sPassword);

	enum {IDD = IDD_LOGIN};

	BEGIN_MSG_MAP(CLoginDlg)
		MESSAGE_HANDLER(WM_INITDIALOG, OnInitDialog)
		COMMAND_HANDLER(IDC_SIGN_IN, BN_CLICKED, OnSignIn)
		COMMAND_HANDLER(IDC_USERNAME_COMBO, CBN_SETFOCUS, OnFocusChange)
		COMMAND_HANDLER(IDC_PASSWORD_EDIT, EN_SETFOCUS, OnFocusChange)
		COMMAND_HANDLER(IDC_SIGN_IN, BN_SETFOCUS, OnFocusChange)
	END_MSG_MAP()

	LRESULT OnInitDialog(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled);
	LRESULT OnSignIn(WORD wNotifyCode, WORD wID, HWND hWndCtl, BOOL& bHandled);
	LRESULT OnFocusChange(WORD wNotifyCode, WORD wID, HWND hWndCtl, BOOL& bHandled);
};
