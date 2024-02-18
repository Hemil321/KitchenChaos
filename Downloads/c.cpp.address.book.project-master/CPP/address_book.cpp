#include <iostream>
#include <string>
#include "record.h"
#include "llist.h"
#include "address_book.h"
using namespace std;

void run_program()
{
    llist records(get_file_name());
    string input_name = "";
    string input_address = "";
    string input_phone = "";
    string dummy = "";
    int input_year = 0;
    int menu_selection = 0;
    display_commands();
    do
    {
        menu_selection = get_menu_selection();
        switch (menu_selection)
        {
        case 1:
            cout << "--------------------\n";
            cout << "Add a new Record\n";
            input_name = get_name();
            input_address = get_address();
            input_year = get_birth_year();
            input_phone = get_phone_number();
            records.add_record(input_name, input_address, input_year, input_phone);
            break;
        case 2:
            cout << "--------------------\n";
            cout << "Print Record(s) with the same name\n";
            input_name = get_name();
            records.print_record(input_name);
            break;
        case 3:
            cout << "--------------------\n";
            cout << "Modify Record(s) with the same name\n";
            input_name = get_name();
            input_address = get_address();
            getline(cin, dummy);
            input_phone = get_phone_number();
            records.modify_record(input_name, input_address, input_phone);
            break;
        case 4:
            cout << "--------------------\n";
            cout << "Printing Records...\n";
            records.print_all_records();
            break;
        case 5:
            cout << "--------------------\n";
            cout << "Delete Record(s) with the same name\n";
            input_name = get_name();
            records.delete_record(input_name);
            break;
        case 6:
            cout << "--------------------\n";
            cout << "Reversing the order of all the Records...\n";
            records.reverse_llist();
            break;
        }
        if (menu_selection != 7)
        {
            display_commands();
        }                          
    } while (menu_selection != 7);
    return;                        
}

void debug_function(int function_call, string name, string address, string phone_number, int year)
{
    cout << "********************\n";
    if (function_call == 1)
    {
        cout << "llist::addRecord( " << name << ", " << address << ", " << year << ", " << phone_number << " );\n";
    }
    else if (function_call == 2)
    {
        cout << "llist::printRecord( " << name << " )n";
    }
    else if (function_call == 3)
    {
        cout << "llist::modifyRecord( " << name << ", " << address << ", " << phone_number << " );\n";
    }
    else if (function_call == 4)
    {
        cout << "llist::printAll( );\n";
    }
    else if (function_call == 5)
    {
        cout << "llist::deleteRecord( " << name << " )n";
    }
    else if (function_call == 6)
    {
        cout << "llist::reverse( );\n";
    }
    cout << "********************\n";
    return;
}

void display_commands()
{
    cout << "Usage:\n";
    cout << "\t1: Add a new record into the database.\n";
    cout << "\t2: Print information about a record using the name as the key. \n";
    cout << "\t3: Modify a record in the database using the name as the key. \n";
    cout << "\t4: Print all information in the database.\n";
    cout << "\t5: Delete an existing record from the database.\n";
    cout << "\t6: Reverse the order of all existing records from the database.\n";
    cout << "\t7: Quit the program.\n";
    cout << "What would you like to do?\n";
    return;
}

int get_menu_selection()
{
    int selection = 0;
    cin >> selection;
    while (cin.fail() || selection < 1 || selection > 7)
    {
        if (cin.fail())
        {
            cout << "Your input was not an integer. Please enter an integer: ";
        }
        else if (selection < 1 || selection > 7)
        {
            cout << "Not a valid choice.\n";
            display_commands();
        }
        cin.clear();
        cin.ignore(256, '\n');
        cin >> selection;
    }                 
    cin.clear();      
    return selection;
}

string get_name()
{
    string name = "";
    cin.clear();
    cin.ignore(256, '\n');
    cout << "Please enter the person's NAME: ";
    getline(cin, name);
    return name;
}

string get_address()
{
    string address = "";
    cout << "Please enter the person's ADDRESS (Enter '$' when you're finished):\n";
    getline(cin, address, '$');
    return address; 
}

int get_birth_year()
{
    int birth_year = 0;
    cout << "Please enter the person's BIRTH YEAR: ";
    cin >> birth_year;
    while (cin.fail())
    { 
        cout << "Not an integer, please enter an integer: ";
        cin.clear();
        cin.ignore(256, '\n');
        cin >> birth_year;
    } 
    cin.clear();
    cin.ignore(256, '\n');
    return birth_year; 
}

string get_phone_number()
{
    string phone_number = "";
    cout << "Please enter the contact's TELEPHONE NUMBER: ";
    getline(cin, phone_number);
    return phone_number; 
}

string get_file_name()
{
    string file_name = "";
    int count;
    cout << "Please enter your FILE NAME ('.txt' file extension will be added): ";
    getline(cin, file_name);
    file_name += ".txt";
    return file_name; 
}
