#include "StdAfx.h"
#include "AudioDlg.h"
#include "MainWnd.h"

const static int g_iControlTextLength = 64;

CChangeAudioDlg::CChangeAudioDlg(PttLib::ISettings2* _pAudioSettings) :
	m_pAudioSettings(_pAudioSettings)
{
}

CChangeAudioDlg::~CChangeAudioDlg()
{
}

void CChangeAudioDlg::_LoadAudioDevices(PttLib::IAudioDevices* pDevs, UINT uCbId, LPCWSTR wszActiveDevice, std::list<std::wstring>* pLst)
{
	pLst->clear();
	SendDlgItemMessage(uCbId, CB_RESETCONTENT, 0, 0);
	int nCount = 0;
	pDevs->get_Count(&nCount);
	WPARAM wSelectedDeviceId = 0;
	for(int i=0;i<nCount;++i) {
		BSTR bstrName;
		BSTR bstrId;
		pDevs->get_Name(i, &bstrName);
		pDevs->get_Id(i, &bstrId);
		pLst->push_back(bstrId);
		std::wstring strName(bstrName);
		if(0==lstrcmp(wszActiveDevice,bstrId)) {
			strName.insert(0,L"*** ");
			wSelectedDeviceId = static_cast<WPARAM>(i);
		}
		SendDlgItemMessage(uCbId, CB_ADDSTRING, 0, reinterpret_cast<LPARAM>(strName.c_str()));
		SysFreeString(bstrName);
		SysFreeString(bstrId);
	}
	SendDlgItemMessage(uCbId, CB_SETCURSEL, wSelectedDeviceId, NULL);
}
/*
				int nCount2 = 0;
				pRecordDevices->get_Count(&nCount2);
				BSTR str11,str12;
				std::wstring s;
				int i=0;
				for(i=0;i<nCount1;++i)
				{
					pPlaybackDevices->get_Name(i, &str11);
					pPlaybackDevices->get_Id(i, &str12);

}
*/
void CChangeAudioDlg::LoadValues()
{
	PttLib::IAudioDevices* pPlaybackDevices = NULL;
	PttLib::IAudioDevices* pRecordDevices = NULL;
	m_pAudioSettings->GetPlaybackDevices(&pPlaybackDevices);
	m_pAudioSettings->GetRecordingDevices(&pRecordDevices);

	BSTR strPlaybackDeviceId;
	BSTR strRecordingDeviceId;
	m_pAudioSettings->get_PlaybackDeviceId(&strPlaybackDeviceId);
	m_pAudioSettings->get_RecordingDeviceId(&strRecordingDeviceId);

	_LoadAudioDevices(pPlaybackDevices, IDC_PLAYBACK_COMBO, strPlaybackDeviceId, &m_lstPlayAudioIds);
	_LoadAudioDevices(pRecordDevices, IDC_RECORDING_COMBO, strRecordingDeviceId, &m_lstRecAudioIds);

	SysFreeString(strPlaybackDeviceId);
	SysFreeString(strRecordingDeviceId);

	pPlaybackDevices->Release();
	pRecordDevices->Release();
/*
	int nCount1 = 0;
	pPlaybackDevices->get_Count(&nCount1);
				int nCount2 = 0;
				pRecordDevices->get_Count(&nCount2);
				BSTR str11,str12;
				std::wstring s;
				int i=0;
				for(i=0;i<nCount1;++i)
				{
					pPlaybackDevices->get_Name(i, &str11);
					pPlaybackDevices->get_Id(i, &str12);
					s.append(str11).append(L"; ");
				
//					SysFreeString(str1);
				
//					SysFreeString(str2);
				}
				s.append(L"<><><><>");
				for(i=0;i<nCount2;++i)
				{
					pRecordDevices->get_Name(i, &str11);
					pRecordDevices->get_Id(i, &str12);
					s.append(str12).append(L"; ");
				
//					SysFreeString(str1);
				
//					SysFreeString(str2);
				}
*/
}


LRESULT CChangeAudioDlg::OnInitDialog(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled)
{
	LoadValues();
	return 0;
}

LRESULT CChangeAudioDlg::OnApplyChange(WORD wNotifyCode, WORD wID, HWND hWndCtl, BOOL& bHandled)
{
	int nIdx = SendDlgItemMessage(IDC_PLAYBACK_COMBO, CB_GETCURSEL, 0, 0);
	if(nIdx>=0) {
		std::list<std::wstring>::const_iterator cit = m_lstPlayAudioIds.begin();
		std::advance(cit,nIdx);
		BSTR bstr = SysAllocString(cit->c_str());
		m_pAudioSettings->put_PlaybackDeviceId(bstr);
		SysFreeString(bstr);
	}

	nIdx = SendDlgItemMessage(IDC_RECORDING_COMBO, CB_GETCURSEL, 0, 0);
	if(nIdx>=0) {
		std::list<std::wstring>::const_iterator cit = m_lstRecAudioIds.begin();
		std::advance(cit,nIdx);
		BSTR bstr = SysAllocString(cit->c_str());
		m_pAudioSettings->put_RecordingDeviceId(bstr);
		SysFreeString(bstr);
	}

	LoadValues();
	return 0;
}

LRESULT CChangeAudioDlg::OnClear(WORD wNotifyCode, WORD wID, HWND hWndCtl, BOOL& bHandled)
{
	BSTR bstr = SysAllocString(L"");
	if(IDC_BUTTONCLEAR1==wID)
		m_pAudioSettings->put_PlaybackDeviceId(bstr);
	else if(IDC_BUTTONCLEAR2==wID)
		m_pAudioSettings->put_RecordingDeviceId(bstr);
	SysFreeString(bstr);
	return 0;
}

LRESULT CChangeAudioDlg::OnClose(WORD wNotifyCode, WORD wID, HWND hWndCtl, BOOL& bHandled)
{
	EndDialog(0);
	return 0;
}

