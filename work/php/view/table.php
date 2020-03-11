<?php if ($res1 ?? null) : ?>
        <table>
            <thead>
                <tr>
                    <th>Id</id>
                    <th>Label</th>
                    <th>Type</th>
                </tr>
            </thead>
            <tbody>
                <?php 
                $i = 0;
                $arr[0] = 0;
                foreach ($res1 as $row) :  
                    $i++; 
                    $arr[$i] = $row["id"];?>
                    <tr id = <?= "tr".$i ?>>
                        <td><?= $row["id"] ?></td>
                        <td><?= $row["label"] ?></td>
                        <td><?= $row["type"] ?></td>
                    </tr>
                    
                <?php endforeach; ?>
                <?php 
                for( $j= 1 ; $j <= $i ; $j++ )
                {?>
                    <script>selectRow('<?= "tr".$j ?>','<?=$arr["$j"]?>' , "<?=$_SERVER['PHP_SELF']?>");</script>
                <?php } ?>
            </tbody>    
        </table>
    <?php endif; ?>