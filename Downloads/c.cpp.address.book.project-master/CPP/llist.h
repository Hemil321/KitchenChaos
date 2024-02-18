#ifndef LLIST_H
#define LLIST_H
#include <string>
class llist {
    private:
        record *start;
        std::string file_name;
        int read_file();
        int write_file();
        record* reverse_llist(record *);
        void delete_all_records();

    public:
        llist();
        llist(std::string);
        ~llist();
        int add_record(std::string, std::string, int, std::string);
        int print_record(std::string);
        int modify_record(std::string, std::string, std::string);
        void print_all_records();
        int delete_record(std::string);
        void reverse_llist();
};
#endif
