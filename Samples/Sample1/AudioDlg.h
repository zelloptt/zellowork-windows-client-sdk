#pragma once

#include <list>
#include "resource.h"
#include <atlapp.h>
#include <atldlgs.h>

namespace PttLib
{
	struct ISettings2;
	struct IAudioDevices;
}

class CChangeAudioDlg :
	public CDialogImpl<CChangeAudioDlg>
{
	PttLib::ISettings2* m_pAudioSettings;
	std::list<std::wstring> m_lstPlayAudioIds,m_lstRecAudioIds;
public:
	CChangeAudioDlg(PttLib::ISettings2* _pAudioSettings);
	~CChangeAudioDlg();
	
	void _LoadAudioDevices(PttLib::IAudioDevices* pDevs, UINT uCbId, LPCWSTR wszActiveDevice, std::list<std::wstring>* pLst);
	void LoadValues();

	enum {IDD = IDD_AUDIO};

	BEGIN_MSG_MAP(CChangeAudioDlg)
		MESSAGE_HANDLER(WM_INITDIALOG, OnInitDialog)
		COMMAND_HANDLER(IDC_CHANGEAUDIO, BN_CLICKED, OnApplyChange)
		COMMAND_HANDLER(IDC_BUTTONCLEAR1, BN_CLICKED, OnClear)
		COMMAND_HANDLER(IDC_BUTTONCLEAR2, BN_CLICKED, OnClear)
		COMMAND_HANDLER(IDOK, BN_CLICKED, OnClose)
		COMMAND_HANDLER(IDCANCEL, BN_CLICKED, OnClose)
	END_MSG_MAP()

	LRESULT OnInitDialog(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled);
	LRESULT OnApplyChange(WORD wNotifyCode, WORD wID, HWND hWndCtl, BOOL& bHandled);
	LRESULT OnClose(WORD wNotifyCode, WORD wID, HWND hWndCtl, BOOL& bHandled);
	LRESULT OnClear(WORD wNotifyCode, WORD wID, HWND hWndCtl, BOOL& bHandled);
};
