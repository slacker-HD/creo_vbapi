// TkDllEample.cpp : 定义 DLL 的初始化例程。
//

#include "stdafx.h"
#include "TkDllEample.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

//
//TODO: 如果此 DLL 相对于 MFC DLL 是动态链接的，
//		则从此 DLL 导出的任何调入
//		MFC 的函数必须将 AFX_MANAGE_STATE 宏添加到
//		该函数的最前面。
//
//		例如:
//
//		extern "C" BOOL PASCAL EXPORT ExportedFunction()
//		{
//			AFX_MANAGE_STATE(AfxGetStaticModuleState());
//			// 此处为普通函数体
//		}
//
//		此宏先于任何 MFC 调用
//		出现在每个函数中十分重要。这意味着
//		它必须作为函数中的第一个语句
//		出现，甚至先于所有对象变量声明，
//		这是因为它们的构造函数可能生成 MFC
//		DLL 调用。
//
//		有关其他详细信息，
//		请参阅 MFC 技术说明 33 和 58。
//

// CTkDllEampleApp

BEGIN_MESSAGE_MAP(CTkDllEampleApp, CWinApp)
END_MESSAGE_MAP()

// CTkDllEampleApp 构造

CTkDllEampleApp::CTkDllEampleApp()
{
    // TODO: 在此处添加构造代码，
    // 将所有重要的初始化放置在 InitInstance 中
}

// 唯一的一个 CTkDllEampleApp 对象

CTkDllEampleApp theApp;

// CTkDllEampleApp 初始化

BOOL CTkDllEampleApp::InitInstance()
{
    CWinApp::InitInstance();
    return TRUE;
}

ProError ShowDialog(wchar_t *Message)
{
    ProUIMessageButton *buttons;
    ProUIMessageButton user_choice;
    ProArrayAlloc(1, sizeof(ProUIMessageButton), 1, (ProArray *)&buttons);
    buttons[0] = PRO_UI_MESSAGE_OK;
    ProUIMessageDialogDisplay(PROUIMESSAGE_INFO, L"Warning", Message, buttons, PRO_UI_MESSAGE_OK, &user_choice);
    ProArrayFree((ProArray *)&buttons);
    return PRO_TK_NO_ERROR;
}

extern "C" int user_initialize()
{
    ProError status;
    status = ShowDialog(L"程序已启动");
    return PRO_TK_NO_ERROR;
}
extern "C" void user_terminate()
{
    ProError status;
    status = ShowDialog(L"程序已退出");
}

//输入一个Int值，返回其平方数
extern "C" PRO_TK_DLL_EXPORT ProError MyPow(ProArgument *input_args, ProArgument **output_args)
{
    ProError status;
    int inputargs = 0;
    ProArgument arg;
	//确定输入正确的参数，一个参数，参数类型为整形
    status = ProArraySizeGet(input_args, &inputargs);
    if (inputargs != 1)
        return PRO_TK_INVALID_ITEM;
    if (input_args[0].value.type != PRO_VALUE_TYPE_INT)
        return PRO_TK_INVALID_ITEM;
	//申请返回值内存空间，只返回一个整形值
    status = ProArrayAlloc(0, sizeof(ProArgument), 1, (ProArray *)output_args);
    if (status == PRO_TK_NO_ERROR)
    {
        arg.value.type = PRO_VALUE_TYPE_INT;
        arg.value.v.i = input_args[0].value.v.i * input_args[0].value.v.i;
        ProArrayObjectAdd((ProArray *)output_args, -1, 1, &arg);
        return PRO_TK_NO_ERROR;
    }
    else
        return PRO_TK_GENERAL_ERROR;
    return PRO_TK_NO_ERROR;
}
