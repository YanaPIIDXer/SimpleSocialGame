class CreateExpansions < ActiveRecord::Migration[5.2]
  def change
    create_table :expansions do |t|
      t.string :bundle_uri, not_null: true
      t.integer :cost, not_null: true
      t.timestamps
    end
  end
end
