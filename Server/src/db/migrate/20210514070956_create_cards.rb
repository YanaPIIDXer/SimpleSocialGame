class CreateCards < ActiveRecord::Migration[5.2]
  def change
    create_table :cards do |t|
      t.string :asset_name, not_null: true
      t.timestamps
    end
  end
end
