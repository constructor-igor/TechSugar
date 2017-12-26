


def update_dic(d):
    d["test"] = "2"


if __name__ == "__main__":
    d = dict()
    d["test"] = "1"
    print("before update: ", d)
    update_dic(d)
    print("after update: ", d)
