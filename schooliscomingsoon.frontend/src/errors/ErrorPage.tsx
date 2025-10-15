export default function ErrorPage() {
  const params = new URLSearchParams(window.location.search);
  const errorId = params.get("errorId");
  return (
    <div className='error_block'>
      <h1>Ошибка входа</h1>
      <p>Код ошибки: {errorId} dasdasgdfsadfddasdasgdfsadfddasdasgdfsadfddasdasgdfsadfddasdasgdfsadfddasdasgdfsadfddasdasgdfsadfddasdasgdfsadfddasdasgdfsadfddasdasgdfsadfddasdasgdfsadfddasdasgdfsadfddasdasgdfsadfddasdasgdfsadfddasdasgdfsadfddasdasgdfsadfddasdasgdfsadfddasdasgdfsadfddasdasgdfsadfd</p>
      <p>Проверьте, что вы вошли через правильный адрес или повторите попытку.</p>
    </div>
  );
}