#include "stdafx.h"
#include "resource.h"
#include "MainForm.h"


CMainForm::CMainForm()
:m_Mode(mLogin), m_State(sUnpushed), m_wExitCode(0)
{

}

LRESULT CMainForm::OnInitDialog(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled)
{
	HRESULT hr = m_spMesh.CoCreateInstance(ZelloPTTLib::CLSID_ZelloClient);
	if (SUCCEEDED(hr) && m_spMesh) {
		ATL::CComPtr<ZelloPTTLib::INetwork> spNetwork;
		m_spMesh->get_Network(&spNetwork);
		if (spNetwork) {
			spNetwork->put_NetworkName(ATL::CComBSTR("default"));
			spNetwork->put_LoginServer(ATL::CComBSTR("default.loudtalks.net"));
			spNetwork->EnableTls(ATL::CComBSTR("tls.zellowork.com"));
		}
		ATL::CComPtr<ZelloPTTLib::ISettings> spSettings;
		m_spMesh->get_Settings(&spSettings);
		if (spSettings)	{
			spSettings->put_ShowIncomingNotification(false);
			spSettings->put_ShowOutgoingNotification(false);
		}
		hr = DispEventAdvise(m_spMesh);
	} else {
		MessageBox(_T("Faled to create PTT Control."), _T("PTT Sample 8"), MB_ICONHAND);
		PostMessage(WM_COMMAND, IDCANCEL);
	}

	m_stUsername = GetDlgItem(IDC_USERNAME_STATIC);
	m_cbUsername = GetDlgItem(IDC_USERNAME_COMBO);
	m_stPassword = GetDlgItem(IDC_PASSWORD_STATIC);
	m_edPassword = GetDlgItem(IDC_PASSWORD_EDIT);
	m_lvContacts = GetDlgItem(IDC_CONTACTS);
	m_btButton = GetDlgItem(IDC_BUTTON);
	m_stStatus = GetDlgItem(IDC_STATUS);

	HIMAGELIST il = ImageList_Create(16, 16, ILC_COLOR24 | ILC_MASK, 1, 1);
	HBITMAP hbm = (HBITMAP)LoadImage(GetModuleHandle(0), MAKEINTRESOURCE(IDB_BUDDY), IMAGE_BITMAP, 0, 0, LR_DEFAULTCOLOR);
	ImageList_AddMasked(il, hbm, RGB(255, 0, 255));
	DeleteObject(hbm);
	m_lvContacts.SendMessage(LVM_SETIMAGELIST, LVSIL_SMALL, (LPARAM)il);

	UpdateMode();
	UpdateStatus();

	m_cbUsername.SendMessage(CB_ADDSTRING, 0, (LPARAM) _T("test1"));
	m_cbUsername.SendMessage(CB_ADDSTRING, 0, (LPARAM) _T("test2"));
	m_cbUsername.SendMessage(CB_ADDSTRING, 0, (LPARAM) _T("test3"));
	m_cbUsername.SendMessage(CB_ADDSTRING, 0, (LPARAM) _T("test4"));
	m_cbUsername.SendMessage(CB_ADDSTRING, 0, (LPARAM) _T("test5"));
	m_cbUsername.SendMessage(CB_ADDSTRING, 0, (LPARAM) _T("test6"));
	m_cbUsername.SendMessage(CB_ADDSTRING, 0, (LPARAM) _T("test7"));
	m_cbUsername.SetWindowText(_T("test5"));
	m_edPassword.SetWindowText(_T("test"));

	SetTimer(1, 200);

	return 1;
}

LRESULT CMainForm::OnDestroy(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled)
{
	if (m_spMesh)
	{
		m_spMesh->SignOut ();
		DispEventUnadvise(m_spMesh);
		m_spMesh.Release();
	}
	return 0;
}

bool CMainForm::BeginSigningOut()
{
	if (m_spMesh)
    {
        ZelloPTTLib::NETWORK_STATUS Status = ZelloPTTLib::NSOFFLINE;
        if (SUCCEEDED(m_spMesh->get_NetworkStatus(&Status)))
        {
            if (ZelloPTTLib::NSOFFLINE != Status)
            {
                if (ZelloPTTLib::NSONLINE == Status || ZelloPTTLib::NSSIGNINGIN == Status)
                {
                    return SUCCEEDED(m_spMesh->SignOut());
                } else return true;//sign out is in progress, wait for completion
            }
        }
    }
	return false;
}

LRESULT CMainForm::OnCommand(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled)
{
	switch (HIWORD (wParam))
	{
	case BN_CLICKED:
		switch (LOWORD (wParam))
		{
		case IDOK:
		case IDCANCEL:
			{
				if(!BeginSigningOut()) {
					EndDialog(LOWORD (wParam));
				} else {
					m_wExitCode = LOWORD (wParam);
				}
			}
			break;
		case IDC_BUTTON:
			if (m_Mode == mLogin)
			{
				TCHAR un[100], pw[100];
				m_cbUsername.GetWindowText(un, 100);
				m_edPassword.GetWindowText(pw, 100);
				HRESULT hr = m_spMesh->put_OnlineStatus (ZelloPTTLib::OSAVAILABLE);
				hr = m_spMesh->SignIn(CComBSTR(un), CComBSTR(pw), false);
			}
			break;
		}
		break;
	}
	return 0;
}

LRESULT CMainForm::OnSize(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled)
{
	static const int nSpace = 4;
	CRect rc, rcs;
	WORD wWidth = LOWORD (lParam), wHeight = HIWORD (lParam);

	m_stStatus.GetWindowRect(&rcs);
	m_stStatus.MoveWindow(nSpace, wHeight - nSpace - rcs.Height(), wWidth - nSpace * 2, rcs.Height());

	m_btButton.GetWindowRect(&rc);
	ScreenToClient(&rc);
	rc.OffsetRect(0, wHeight - nSpace * 2 - rcs.Height() - rc.bottom);
	m_btButton.MoveWindow(max(nSpace, (wWidth - rc.Width()) / 2), rc.top, rc.Width(), rc.Height());
	m_lvContacts.MoveWindow(nSpace, nSpace, wWidth - nSpace * 2, rc.top - nSpace * 2);

	m_stUsername.GetWindowRect(&rc);
	ScreenToClient(&rc);
	m_stUsername.MoveWindow(max(nSpace, (wWidth - rc.Width()) / 2), rc.top, rc.Width(), rc.Height());

	m_cbUsername.GetWindowRect(&rc);
	ScreenToClient(&rc);
	m_cbUsername.MoveWindow(max(nSpace, (wWidth - rc.Width()) / 2), rc.top, rc.Width(), rc.Height());

	m_stPassword.GetWindowRect(&rc);
	ScreenToClient(&rc);
	m_stPassword.MoveWindow(max(nSpace, (wWidth - rc.Width()) / 2), rc.top, rc.Width(), rc.Height());

	m_edPassword.GetWindowRect(&rc);
	ScreenToClient(&rc);
	m_edPassword.MoveWindow(max(nSpace, (wWidth - rc.Width()) / 2), rc.top, rc.Width(), rc.Height());
	return 0;
}

LRESULT CMainForm::OnTimer(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled)
{
	if (1 == wParam)
	{
		UpdateStatus();
		if (m_Mode == mContacts)
		{
			if ((m_btButton.SendMessage(BM_GETSTATE) & BST_PUSHED) == BST_PUSHED && m_State != sPushed)
			{
				m_State = sPushed;
				BeginMessage();
			}
			if ((m_btButton.SendMessage(BM_GETSTATE) & BST_PUSHED) != BST_PUSHED && m_State != sUnpushed)
			{
				m_State = sUnpushed;
				EndMessage();
			}
		}
	}
	return 0;
}

LRESULT CMainForm::OnListViewItemActivate (int idCtrl, LPNMHDR pnmh, BOOL& bHandled)
{
	if(m_lvContacts.SendMessage(LVM_GETSELECTEDCOUNT) == 1)
	{
		int n = m_lvContacts.SendMessage(LVM_GETNEXTITEM, -1, LVNI_SELECTED);
		std::wstring sId = m_Ids[n];
		if(!sId.empty())
		{
			ATL::CComPtr<ZelloPTTLib::IContacts> spContacts;
			if (m_spMesh && SUCCEEDED(m_spMesh->get_Contacts(&spContacts)) && spContacts)
			{
				ATL::CComPtr<ZelloPTTLib::IContact> spContact;
				spContacts->Find(CComBSTR(sId.c_str ()), &spContact);
				if(spContact)
				{
					ATL::CComPtr<ZelloPTTLib::IChannel> spChannel;
					spContact->QueryInterface(&spChannel);
					if(spChannel)
					{
						// Connect/disconnect channel
						ZelloPTTLib::ONLINE_STATUS status = ZelloPTTLib::OSOFFLINE;
						spChannel->get_Status(&status);
						if(status == ZelloPTTLib::OSOFFLINE)
							spChannel->Connect();
						else
							spChannel->Disconnect();
					}
				}
			}
		}
	}
	return 0;
}

void CMainForm::UpdateStatus()
{
	ZelloPTTLib::NETWORK_STATUS ns = ZelloPTTLib::NSOFFLINE;
	if (m_spMesh)
		m_spMesh->get_NetworkStatus(&ns);
	TCHAR tszStatus[128] = {0};
	switch (ns)
	{
	case ZelloPTTLib::NSOFFLINE:
		LoadString(GetModuleHandle(0), IDS_OFFLINE, tszStatus, _countof(tszStatus) - 1);
		break;
	case ZelloPTTLib::NSONLINE:
		LoadString(GetModuleHandle(0), IDS_ONLINE, tszStatus, _countof(tszStatus) - 1);
		break;
	case ZelloPTTLib::NSSIGNINGIN:
		LoadString(GetModuleHandle(0), IDS_SIGNING_IN, tszStatus, _countof(tszStatus) - 1);
		break;
	case ZelloPTTLib::NSSIGNINGOUT:
		LoadString(GetModuleHandle(0), IDS_SIGNING_OUT, tszStatus, _countof(tszStatus) - 1);
		break;
	}
	m_stStatus.SetWindowText(tszStatus);
	if ((ns == ZelloPTTLib::NSOFFLINE || ns == ZelloPTTLib::NSSIGNINGIN) && m_Mode != mLogin)
	{
		m_Mode = mLogin;
		UpdateMode();
	}
	if ((ns == ZelloPTTLib::NSONLINE || ns == ZelloPTTLib::NSSIGNINGOUT) && m_Mode != mContacts)
	{
		m_Mode = mContacts;
		UpdateMode();
	}
}

void CMainForm::UpdateMode()
{
	TCHAR tszButton[128] = {0};
	if (m_Mode == mLogin)
	{
		m_stUsername.ShowWindow(SW_SHOW);
		m_cbUsername.ShowWindow(SW_SHOW);
		m_stPassword.ShowWindow(SW_SHOW);
		m_edPassword.ShowWindow(SW_SHOW);
		m_lvContacts.ShowWindow(SW_HIDE);
		LoadString(GetModuleHandle(0), IDS_SIGN_IN, tszButton, _countof(tszButton) - 1);
	}
	else if (m_Mode = mContacts)
	{
		m_stUsername.ShowWindow(SW_HIDE);
		m_cbUsername.ShowWindow(SW_HIDE);
		m_stPassword.ShowWindow(SW_HIDE);
		m_edPassword.ShowWindow(SW_HIDE);
		m_lvContacts.ShowWindow(SW_SHOW);
		LoadString(GetModuleHandle(0), IDS_SPEAK, tszButton, _countof(tszButton) - 1);
		UpdateContacts();
	}
	m_btButton.SetWindowText(tszButton);
}

void CMainForm::UpdateContacts()
{
	m_Ids.clear();
	ATL::CComPtr<ZelloPTTLib::IContacts> spContacts;
	if (m_spMesh && SUCCEEDED(m_spMesh->get_Contacts(&spContacts)) && spContacts)
	{
		int cnt = 0;
		spContacts->get_Count(&cnt);
		int nMsg = cnt == m_lvContacts.SendMessage(LVM_GETITEMCOUNT) ? LVM_SETITEM : LVM_INSERTITEM;
		if (nMsg == LVM_INSERTITEM)
		{
			m_lvContacts.SetRedraw(0);
			m_lvContacts.SendMessage(LVM_DELETEALLITEMS);
		}
		for (int i = 0; i < cnt; ++i)
		{
			ATL::CComPtr<ZelloPTTLib::IContact> spContact;
			if (SUCCEEDED(spContacts->get_Item(i, &spContact)) && spContact)
			{
				CComBSTR name, fullName, id;
				spContact->get_Name(&name);
				spContact->get_FullName(&fullName);
				spContact->get_Id(&id);
				m_Ids[i] = std::wstring(id ? id : L"");
				ZelloPTTLib::ONLINE_STATUS status = ZelloPTTLib::OSOFFLINE;
				spContact->get_Status(&status);
				ZelloPTTLib::CONTACT_TYPE type = ZelloPTTLib::CTUSER;
				spContact->get_Type(&type);
				LV_ITEM li = {0};
				TCHAR tszText[128] = {0}, tszStatus[64] = {0};
				li.mask = LVIF_TEXT | LVIF_IMAGE;
				li.iItem = i;
				li.pszText = tszText;
				LPCWSTR pName = (fullName.m_str && *fullName.m_str) ? fullName.m_str : name.m_str;
				switch (type)
				{
				case ZelloPTTLib::CTGROUP:
					{
						li.iImage = 5;
						int iOnline = 0, iCount = 0;
						ATL::CComPtr<ZelloPTTLib::IGroup> spGroup;
						spContact->QueryInterface(&spGroup);
						if(spGroup)
						{
							spGroup->get_Count(&iCount);
							spGroup->get_OnlineCount(&iOnline);
						}
						if(iOnline > 0)
							_sntprintf(tszStatus, _countof(tszStatus) - 1, _T("%i/%i"), iOnline, iCount);
						else
							LoadString(GetModuleHandle(0), IDS_OFFLINE, tszStatus, _countof(tszStatus) - 1);
						break;
					}
				case ZelloPTTLib::CTCHANNEL:
					{
						li.iImage = (status == ZelloPTTLib::OSOFFLINE || status == ZelloPTTLib::OSCONNECTING) ? 7 : 6;
						if(status == ZelloPTTLib::OSCONNECTING)
							LoadString(GetModuleHandle(0), IDS_CONNECTING, tszStatus, _countof(tszStatus) - 1);
						else
						if(status == ZelloPTTLib::OSOFFLINE)
							LoadString(GetModuleHandle(0), IDS_OFFLINE, tszStatus, _countof(tszStatus) - 1);
						else
						{
							ATL::CComPtr<ZelloPTTLib::IChannel> spChannel;
							spContact->QueryInterface(&spChannel);
							if(spChannel)
							{
								int iOnline = 0;
								spChannel->get_OnlineCount(&iOnline);
								TCHAR tszUsers[64] = {0};
								LoadString(GetModuleHandle(0), IDS_USERS_IN_CHANNEL, tszUsers, _countof(tszUsers) - 1);
								_sntprintf(tszStatus, _countof(tszStatus) - 1, tszUsers, iOnline);
							}
						}
						break;
					}
				default:
					switch (status)
					{
					case ZelloPTTLib::OSAVAILABLE:
						li.iImage = 0;
						break;
					case ZelloPTTLib::OSBUSY:
						li.iImage = 2;
						break;
					case ZelloPTTLib::OSAWAY:
						li.iImage = 3;
						break;
					case ZelloPTTLib::OSHEADPHONES:
						li.iImage = 4;
						break;
					default:
						li.iImage = 1;
						break;
					}
				}
				if(!*tszStatus)
				{
					switch (status)
					{
					case ZelloPTTLib::OSAVAILABLE:
						LoadString(GetModuleHandle(0), IDS_AVAILABLE, tszStatus, _countof(tszStatus) - 1);
						break;
					case ZelloPTTLib::OSBUSY:
						LoadString(GetModuleHandle(0), IDS_BUSY, tszStatus, _countof(tszStatus) - 1);
						break;
					case ZelloPTTLib::OSAWAY:
						LoadString(GetModuleHandle(0), IDS_AWAY, tszStatus, _countof(tszStatus) - 1);
						break;
					case ZelloPTTLib::OSHEADPHONES:
						LoadString(GetModuleHandle(0), IDS_HEADPHONES, tszStatus, _countof(tszStatus) - 1);
						break;
					default:
						LoadString(GetModuleHandle(0), IDS_OFFLINE, tszStatus, _countof(tszStatus) - 1);
					}
				}
				_sntprintf(tszText, _countof(tszText) - 1, _T("%s (%s)"), ATL::CW2T(pName, CP_ACP).m_psz, tszStatus);
				m_lvContacts.SendMessage(nMsg, 0, (LPARAM)&li);
			}
		}
		if (nMsg == LVM_INSERTITEM)
		{
			m_lvContacts.SetRedraw();
			m_lvContacts.InvalidateRect(0);
		}
	}
}

void  __stdcall CMainForm::OnSignInStarted()
{
	UpdateStatus();
}

void __stdcall CMainForm::OnSignInSucceeded()
{
	UpdateStatus();
}

void __stdcall CMainForm::OnSignInFailed(ZelloPTTLib::CLIENT_ERROR cerr)
{
	UpdateStatus();
}

void __stdcall CMainForm::OnSignOutStarted()
{
	UpdateStatus();
}

void __stdcall CMainForm::OnSignOutComplete()
{
	UpdateStatus();
	if (0 != m_wExitCode) {
		PostMessage(WM_USER);
	}
}

LRESULT CMainForm::OnExitAllowed(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled)
{
	if (0 != m_wExitCode) {
		EndDialog(m_wExitCode);
		m_wExitCode = 0;
	}
	return 0;
}

void __stdcall CMainForm::OnContactListChanged()
{
	UpdateContacts();
}

void __stdcall CMainForm::OnMessageInBegin(ZelloPTTLib::IMessage* pMessage)
{
	ATL::CComQIPtr<ZelloPTTLib::IAudioInMessage> spInMessage(pMessage);
	if(spInMessage) {
		ATL::CComPtr<ZelloPTTLib::IContact> spContact;
		spInMessage->get_Sender(&spContact);
		if (spContact) {
			ATL::CComBSTR bstrId;
			ATL::CComBSTR bstrName;
			spInMessage->get_Id(&bstrId);
			spContact->get_Name(&bstrName);
			TCHAR tszTmp[128] = {0};
			_sntprintf(tszTmp, _countof(tszTmp) - 1, _T("Incoming message %s from %s begins\n"), ATL::CW2T(bstrId.m_str, CP_ACP).m_psz, ATL::CW2T(bstrName.m_str, CP_ACP).m_psz);
			OutputDebugString(tszTmp);
		}
	}
}

void __stdcall CMainForm::OnMessageInEnd(ZelloPTTLib::IMessage* pMessage)
{
	ATL::CComQIPtr<ZelloPTTLib::IAudioInMessage> spInMessage(pMessage);
	if(spInMessage) {
		ATL::CComPtr<ZelloPTTLib::IContact> spContact;
		spInMessage->get_Sender(&spContact);
		if (spContact) {
			ATL::CComBSTR bstrId;
			ATL::CComBSTR bstrName;
			int iDuration = 0;
			spInMessage->get_Id(&bstrId);
			spInMessage->get_Duration(&iDuration);
			spContact->get_Name(&bstrName);
			TCHAR tszTmp[128] = {0};
			_sntprintf(tszTmp, _countof(tszTmp) - 1, _T("Incoming message %s from %s ends, duration %i\n"), ATL::CW2T(bstrId.m_str, CP_ACP).m_psz, ATL::CW2T(bstrName.m_str, CP_ACP).m_psz, iDuration);
			OutputDebugString(tszTmp);
		}
	}
}

void __stdcall CMainForm::OnMessageOutBegin(ZelloPTTLib::IMessage* pMessage, ZelloPTTLib::IContact* pContact)
{
	ATL::CComQIPtr<ZelloPTTLib::IAudioOutMessage> spOutMessage(pMessage);
	if(spOutMessage && pContact) {
		ATL::CComBSTR bstrId;
		ATL::CComBSTR bstrName;
		spOutMessage->get_Id(&bstrId);
		pContact->get_Name(&bstrName);
		TCHAR tszTmp[128] = {0};
		_sntprintf(tszTmp, _countof(tszTmp) - 1, _T("Outgoing message %s to %s begins\n"), ATL::CW2T(bstrId.m_str, CP_ACP).m_psz, ATL::CW2T(bstrName.m_str, CP_ACP).m_psz);
		OutputDebugString(tszTmp);
	}
}

void __stdcall CMainForm::OnMessageOutEnd(ZelloPTTLib::IMessage* pMessage, ZelloPTTLib::IContact* pContact)
{
	ATL::CComQIPtr<ZelloPTTLib::IAudioOutMessage> spOutMessage(pMessage);
	if(spOutMessage && pContact) {
		ATL::CComBSTR bstrId;
		ATL::CComBSTR bstrName;
		int iDuration = 0;
		spOutMessage->get_Id(&bstrId);
		spOutMessage->get_Duration(&iDuration);
		pContact->get_Name(&bstrName);
		TCHAR tszTmp[128] = {0};
		_sntprintf(tszTmp, _countof(tszTmp) - 1, _T("Outgoing message %s to %s ends, duration %i\n"), ATL::CW2T(bstrId.m_str, CP_ACP).m_psz, ATL::CW2T(bstrName.m_str, CP_ACP).m_psz, iDuration);
		OutputDebugString(tszTmp);
	}
}

void __stdcall CMainForm::OnMessageOutError(ZelloPTTLib::IMessage* pMessage, ZelloPTTLib::IContact* pContact)
{
	ATL::CComQIPtr<ZelloPTTLib::IAudioOutMessage> spOutMessage(pMessage);
	if(spOutMessage && pContact) {
		ATL::CComBSTR bstrId;
		ATL::CComBSTR bstrName;
		spOutMessage->get_Id(&bstrId);
		pContact->get_Name(&bstrName);
		TCHAR tszTmp[128] = {0};
		_sntprintf(tszTmp, _countof(tszTmp) - 1, _T("Outgoing message %s to %s error\n"), ATL::CW2T(bstrId.m_str, CP_ACP).m_psz, ATL::CW2T(bstrName.m_str, CP_ACP).m_psz);
		OutputDebugString(tszTmp);
	}
}

void __stdcall CMainForm::OnAudioMessageInStart(ZelloPTTLib::IAudioInMessage* pMessage, VARIANT_BOOL* pbVal)
{
	if(pMessage) {
		ATL::CComPtr<ZelloPTTLib::IContact> spContact;
		pMessage->get_Sender(&spContact);
		if (spContact) {
			ATL::CComBSTR bstrId;
			ATL::CComBSTR bstrName;
			pMessage->get_Id(&bstrId);
			spContact->get_Name(&bstrName);
			TCHAR tszTmp[128] = {0};
			_sntprintf(tszTmp, _countof(tszTmp) - 1, _T("Incoming message %s from %s starts\n"), ATL::CW2T(bstrId.m_str, CP_ACP).m_psz, ATL::CW2T(bstrName.m_str, CP_ACP).m_psz);
			OutputDebugString(tszTmp);
		}
	}
    if (pbVal) {
        *pbVal = VARIANT_TRUE;
    }
}

void __stdcall CMainForm::OnAudioMessageInStop(ZelloPTTLib::IAudioInMessage* pMessage)
{
	if(pMessage) {
		ATL::CComPtr<ZelloPTTLib::IContact> spContact;
		pMessage->get_Sender(&spContact);
		if (spContact) {
			ATL::CComBSTR bstrId;
			ATL::CComBSTR bstrName;
			pMessage->get_Id(&bstrId);
			spContact->get_Name(&bstrName);
			TCHAR tszTmp[128] = {0};
			_sntprintf(tszTmp, _countof(tszTmp) - 1, _T("Incoming message %s from %s stops\n"), ATL::CW2T(bstrId.m_str, CP_ACP).m_psz, ATL::CW2T(bstrName.m_str, CP_ACP).m_psz);
			OutputDebugString(tszTmp);
		}
	}
}

void CMainForm::BeginMessage()
{
	if (!m_spMesh)
		return;
	CComSafeArray<VARIANT> Users;
	unsigned cnt = m_lvContacts.SendMessage(LVM_GETITEMCOUNT);
	for (unsigned i = 0; i < cnt; ++i)
	{
		LVITEM li = {0};
		li.mask = LVIF_STATE;
		li.iItem = i;
		li.stateMask = LVIS_SELECTED;
		int r = m_lvContacts.SendMessage(LVM_GETITEM, 0, (LPARAM)&li);
		if (li.state & LVIS_SELECTED)
		{
			std::wstring id = m_Ids[i];
			if(!id.empty())
				Users.Add(CComVariant(id.c_str ()));
		}
	}
	if(Users.m_psa != 0 && Users.GetCount() > 0)
		m_spMesh->BeginMessage(Users);
}

void CMainForm::EndMessage()
{
	if (m_spMesh)
		m_spMesh->EndMessage();
}

void __stdcall CMainForm::OnEvent(ZelloPTTLib::IZelloEvent* pEvent)
{
    ZelloPTTLib::ZELLO_EVENT_TYPE eEventType;
    ZelloPTTLib::CLIENT_ERROR eErrorCode;
    _bstr_t sEventText;
    pEvent->get_Type(&eEventType);
    pEvent->get_Text(sEventText.GetAddress());
    pEvent->get_ErrCode(&eErrorCode);
}