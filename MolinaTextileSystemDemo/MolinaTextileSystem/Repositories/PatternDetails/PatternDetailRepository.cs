using Dapper;
using MolinaTextileSystem.Data;
using MolinaTextileSystem.Models;
using System.Data;

namespace MolinaTextileSystem.Repositories.PatternDetails
{
	public class PatternDetailRepository : IPatternDetailRepository
	{
		private readonly ISqlDataAccess _dataAccess;

		public PatternDetailRepository(ISqlDataAccess dataAccess)
		{
			_dataAccess = dataAccess;
		}

		public IEnumerable<RawMaterialsModel> GetAllRawMaterials()
		{
			using (var connection = _dataAccess.GetConnection())
			{
				string storeProcedure = "spPRawMaterials_GetAll";

				return
					connection.Query<RawMaterialsModel>(
						storeProcedure,
						commandType: CommandType.StoredProcedure
					);
			}
		}

		public IEnumerable<PatternModel> GetAllPatterns()
		{
			using (var connection = _dataAccess.GetConnection())
			{
				string storeProcedure = "spPPatterns_GetAll";

				return
					connection.Query<PatternModel>(
						storeProcedure,
						commandType: CommandType.StoredProcedure
					);
			}
		}

		public IEnumerable<PatternDetailModel> GetAll()
		{
			using (var connection = _dataAccess.GetConnection())
			{
				string storedProcedure = "spPatternDetails_GetAll";

				var patternDetails = connection.Query<PatternDetailModel, RawMaterialsModel, PatternModel, PatternDetailModel >
					(storedProcedure, (patternDetail, rawMaterial, pattern) => {
						patternDetail.RawMaterial = rawMaterial;
						patternDetail.Pattern = pattern;

						return patternDetail;
					},
					splitOn: "RawMaterialName,PatternName",
					commandType: CommandType.StoredProcedure);

				return patternDetails;
			}
		}

		public PatternDetailModel? GetById(int id)
		{
			using (var connection = _dataAccess.GetConnection())
			{
				string storeProcedure = "spPatternDetails_GetById";

				return
					connection.QueryFirstOrDefault<PatternDetailModel>(
						storeProcedure,
						new { PatternDetailId = id },
						commandType: CommandType.StoredProcedure
					);
			}
		}

		public void Add(PatternDetailModel patternDetail)
		{
			using (var connection = _dataAccess.GetConnection())
			{
				string storeProcedure = "spPatternDetails_Insert";

				connection.Execute(
					storeProcedure,
					new
					{
						patternDetail.RawMaterialQuantity,
						patternDetail.RawMaterialId,
						patternDetail.PatternId
					},
					commandType: CommandType.StoredProcedure
				);
			}
		}
		public void Edit(PatternDetailModel patternDetail)
		{
			using (var connection = _dataAccess.GetConnection())
			{
				string storeProcedure = "spPatternDetails_Update";

				connection.Execute(
					storeProcedure,
					new
					{
						patternDetail.PatternDetailId,
						patternDetail.RawMaterialQuantity,
						patternDetail.RawMaterialId,
						patternDetail.PatternId
					},
					commandType: CommandType.StoredProcedure
				);
			}
		}

		public void Delete(int id)
		{
			using (var connection = _dataAccess.GetConnection())
			{
				string storeProcedure = "spPatternDetails_Delete";

				connection.Execute(
					storeProcedure,
					new { PatternDetailId = id },
					commandType: CommandType.StoredProcedure
				);
			}
		}
	}
}
