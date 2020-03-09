<?php

/**
 * Open the database file and catch the exception it it fails
 *
 * @var array $dsn with connection details
 *
 * @return object database connection
 */
function connectDatabase(array $dsn)
{
    try {
        $db = new PDO(
            $dsn["dsn"],
            $dsn["username"],
            $dsn["password"]
        );

        $db->setAttribute(PDO::ATTR_DEFAULT_FETCH_MODE, PDO::FETCH_ASSOC);
        $db->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
    } catch (PDOException $e) {
        echo "Failed to connect to the database using DSN:<br>\n";
        print_r($dsn);
        throw $e;
    }

    return $db;
}

function searchWildcard($search, $db){
    //$db = connectDatabase($dsn);
    $like = "%$search%";
    // Prepare and execute the SQL statement
    $sql = <<<EOD
    SELECT
    *
    FROM tech
    WHERE
    id = ?
    OR label LIKE ?
    OR type LIKE ?
    ;
    EOD;
    $stmt = $db->prepare($sql);
    $stmt->execute([$search, $like, $like]);

    // Get the results as an array with column names as array keys
    return $stmt->fetchAll();
}

function getAll($db){
    // Prepare and execute the SQL statement
    $sql = <<<EOD
    SELECT
    *
    FROM tech;
    EOD;
    $stmt = $db->prepare($sql);
    $stmt->execute();

    // Get the results as an array with column names as array keys
    return $stmt->fetchAll();
}
