import pandas as pd


if __name__ == "__main__":
    print("[main] started")

    data_1 = {'R2': [11.0, 11.1], 'slope': [12.0, 12.1]}
    data_2 = {'R2': [21.0, 21.1], 'slope': [22.0, 22.1]}
    df_1 = pd.concat({"parameter1": pd.DataFrame(data_1), "parameter2": pd.DataFrame(data_2)}, axis=1, names=["parameters", "merits"])

    columns = ['component1', 'component2']
    df_2 = pd.DataFrame(columns = columns)
    df_2.loc[len(df_2)] = ["enable", "disable"]
    df_2.loc[len(df_2)] = ["disable", "enable"]

    print(df_1)
    df_1.to_csv("df_1.csv")

    print(df_2)
    df_2.to_csv("df_2.csv")

    # df_merged = pd.merge(df_1, df_2, left_on=['parameter1_R2', 'parameter2_R2'], right_on=['component1', 'component2'])
    # df_merged = pd.merge(df_1, df_2, left_index=True, right_index=True)

    name_of_fake_second_level_column = ''
    df_2_to_tup = [(x, name_of_fake_second_level_column) for x in df_2.columns]
    df_2.columns = pd.MultiIndex.from_tuples(df_2_to_tup)
    df_merged = pd.merge(df_1, df_2, left_index=True, right_index=True)

    print(df_merged)
    df_merged.to_csv("df_merged.csv")

    print("[main] finished")