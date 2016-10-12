#include "StdAfx.h"
#include "resourceppc.h"
#include "LoginDlg.h"

const static int g_iControlTextLength = 64;

CLoginDlg::CLoginDlg(CLoginDlgCb* pCb) :
	m_pCb(pCb)
{
}

CLoginDlg::~CLoginDlg()
{
}

void CLoginDlg::ShowFocus()
{
	if (m_wndFocused)
		m_wndFocused.SetFocus();
}

void CLoginDlg::EnableControls(bool bEnable)
{
	GetDlgItem(IDC_USERNAME_COMBO).EnableWindow(bEnable);
	GetDlgItem(IDC_PASSWORD_EDIT).EnableWindow(bEnable);
	GetDlgItem(IDC_SIGN_IN).EnableWindow(bEnable);
	if (bEnable)
		ShowFocus();
}

std::wstring CLoginDlg::GetUsername() const
{
	TCHAR tszVal[g_iControlTextLength + 1] = {0};
	GetDlgItem(IDC_USERNAME_COMBO).GetWindowText(tszVal, _countof(tszVal));
	return std::wstring(ATL::CT2W(tszVal).m_psz);
}

std::wstring CLoginDlg::GetPassword() const
{
	TCHAR tszVal[g_iControlTextLength + 1] = {0};
	GetDlgItem(IDC_PASSWORD_EDIT).GetWindowText(tszVal, _countof(tszVal));
	return std::wstring(ATL::CT2W(tszVal).m_psz);
}

void CLoginDlg::SetUsername(const std::wstring& sUsername)
{
	GetDlgItem(IDC_USERNAME_COMBO).SetWindowText(ATL::CW2T(sUsername.c_str(), CP_ACP).m_psz);
}

void CLoginDlg::AddUsername(const std::wstring& sUsername)
{
	SendDlgItemMessage(IDC_USERNAME_COMBO, CB_ADDSTRING, 0, (LPARAM) ATL::CW2T(sUsername.c_str(), CP_ACP).m_psz);
}

void CLoginDlg::SetPassword(const std::wstring& sPassword)
{
	GetDlgItem(IDC_PASSWORD_EDIT).SetWindowText(ATL::CW2T(sPassword.c_str(), CP_ACP).m_psz);
}

LRESULT CLoginDlg::OnInitDialog(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled)
{
	m_wndFocused = 0;
	GetDlgItem(IDC_USERNAME_COMBO).SendMessage(CB_LIMITTEXT, g_iControlTextLength);
	GetDlgItem(IDC_PASSWORD_EDIT).SendMessage(EM_SETLIMITTEXT, g_iControlTextLength);
	GetNextDlgTabItem(0).SetFocus();
	return 0;
}

LRESULT CLoginDlg::OnSignIn(WORD wNotifyCode, WORD wID, HWND hWndCtl, BOOL& bHandled)
{
	if (m_pCb)
	{
		std::wstring sUsername(GetUsername());
		std::wstring sPassword(GetPassword());
		if (!sUsername.empty() && !sPassword.empty())
			m_pCb->OnSignIn(sUsername, sPassword);
	}
	return 0;
}

LRESULT CLoginDlg::OnFocusChange(WORD wNotifyCode, WORD wID, HWND hWndCtl, BOOL& bHandled)
{
	m_wndFocused = hWndCtl;
	return 0;
}
