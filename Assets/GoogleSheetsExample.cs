using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class GoogleSheetExample : MonoBehaviour
{
    private static readonly string[] Scopes = { SheetsService.Scope.Spreadsheets }; // Phạm vi quyền hạn
    private static readonly string ApplicationName = "PlaceSequenceApp";
    private static readonly string SpreadsheetId = "1Erll59Fy33jusM5otkwU2gjUMusTQRzt_ny58C_A4N8"; // ID Google Sheet
    private static readonly string SheetName = "Sheet1"; // Tên sheet
    private SheetsService service;

    void Start()
    {
        AuthenticateAndInitializeService();
    }

    private void AuthenticateAndInitializeService()
    {
        try
        {
            string credentialsPath = Path.Combine(Application.streamingAssetsPath, "credentials.json");
            using (var stream = new FileStream(credentialsPath, FileMode.Open, FileAccess.Read))
            {
                var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None
                ).Result;

                service = new SheetsService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName,
                });
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Authentication failed: " + ex.Message);
        }
    }
    [Button]
    public void Test()
    {
        var data = new List<object> { "Hello", 123, "World", 456 };
        AddDataToSheetAsync(data.ToArray());
    }

    [Button]
    public async Task AddDataToSheetAsync(params object[] rowData)
    {
        await Task.Run(() =>
        {
            try
            {
                var range = $"{SheetName}!A1"; // Cột bắt đầu ghi
                var valueRange = new ValueRange();
                var formattedRow = new List<object>();

                foreach (var data in rowData)
                {
                    if (data is string || data is int)
                    {
                        formattedRow.Add(data);
                    }
                    else
                    {
                        Debug.LogWarning("Unsupported data type. Only string and int are allowed.");
                        return;
                    }
                }

                valueRange.Values = new List<IList<object>> { formattedRow };
                var appendRequest = service.Spreadsheets.Values.Append(valueRange, SpreadsheetId, range);
                appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.RAW;
                appendRequest.Execute();

                Debug.Log("Data added successfully");
            }
            catch (Exception ex)
            {
                Debug.LogError("Failed to add data: " + ex.Message);
            }
        });
    }
}
