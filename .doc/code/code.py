import sys
import pandas as pd

# Function to convert each sheet in the Excel file to a separate CSV file
def convert_xlsx_to_csv(excel_file,export_path,skip_row=[]):
    # Read the Excel file
    with pd.ExcelFile(excel_file) as excel_data:
        # Loop through each sheet in the Excel file
        for sheet_name in excel_data.sheet_names:
            if sheet_name.startswith("_"):
                continue
            # Read the sheet into a DataFrame
            df = excel_data.parse(sheet_name)
            df = df.drop(index=skip_row)
            # Define the CSV file name based on the sheet name
            csv_file = export_path + f"{sheet_name}.csv"
            
            # Save the DataFrame to a CSV file
            df.to_csv(csv_file, index=False)  # index=False to avoid writing row indices
            
            print(f"Sheet '{sheet_name}' has been converted to '{csv_file}'")
        
    excel_data.close()
    
if __name__ == "__main__":
    excel_path = sys.argv[1]
    csv_export_path = sys.argv[2]
    convert_xlsx_to_csv(excel_path, csv_export_path)